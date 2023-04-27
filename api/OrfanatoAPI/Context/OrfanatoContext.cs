using Microsoft.EntityFrameworkCore;
using OrfanatoAPI.Mapping;
using OrfanatoAPI.Models;

namespace OrfanatoAPI.Context;

public class OrfanatoContext : DbContext
{
    public DbSet<Orphanage> Orfanatos { get; set; }
    public DbSet<Image> Imagens { get; set; }

    public OrfanatoContext(DbContextOptions<OrfanatoContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new OrfanatoMap());
        builder.ApplyConfiguration(new ImagensMap());
        base.OnModelCreating(builder);
    }
}
