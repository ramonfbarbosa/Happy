using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrfanatoAPI.Migrations
{
    public partial class AdicionandoCampoWhatsapp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Whatsapp",
                table: "TAB_ORFANATO",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Whatsapp",
                table: "TAB_ORFANATO");
        }
    }
}
