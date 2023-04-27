using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OrfanatoAPI.Context;
using OrfanatoAPI.Models;
using OrfanatoAPI.Repositories;
using OrfanatoAPI.Services;
using OrfanatoAPI.Services.Auth;
using OrfanatoAPI.Validators;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

string connectionString = builder.Configuration.GetConnectionString("ORFANATO_MANAGER");
builder.Services.AddTransient<IDbConnection>(conn => new SqlConnection(connectionString));
builder.Services.AddDbContext<OrfanatoContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseLazyLoadingProxies();
});

builder.Services.AddTransient<IOrfanatoRepository, OrfanatoRepository>();
builder.Services.AddTransient<IOrfanatoService, OrfanatoService>();
builder.Services.AddTransient<IValidator<Orphanage>, OrfanatoValidator>();

builder.Services.AddTransient<ITokenGenerator, TokenGenerator>();
builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Orfanato API",
        Version = "v1",
        Description = "API construída para ser consumida pelo web da Happy",
        Contact = new OpenApiContact
        {
            Name = "Ramon Barbosa",
            Email = "ramonmfb777@gmail.com",
            Url = new Uri("https://github.com/ramonfbarbosa")
        },
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor utilize Bearer <TOKEN>",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

var secretKey = configuration.GetValue<string>("JWT:Key");

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
