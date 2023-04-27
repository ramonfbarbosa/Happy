using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrfanatoAPI.Migrations
{
    public partial class MapeandoWhatsappNoBanco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Whatsapp",
                table: "TAB_ORFANATO",
                newName: "WHATSAPP");

            migrationBuilder.AlterColumn<string>(
                name: "WHATSAPP",
                table: "TAB_ORFANATO",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WHATSAPP",
                table: "TAB_ORFANATO",
                newName: "Whatsapp");

            migrationBuilder.AlterColumn<string>(
                name: "Whatsapp",
                table: "TAB_ORFANATO",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)");
        }
    }
}
