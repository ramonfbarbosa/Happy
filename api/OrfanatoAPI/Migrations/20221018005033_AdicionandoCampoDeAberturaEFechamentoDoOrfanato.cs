using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrfanatoAPI.Migrations
{
    public partial class AdicionandoCampoDeAberturaEFechamentoDoOrfanato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HORA_DE_ABERTURA",
                table: "TAB_ORFANATO");

            migrationBuilder.AlterColumn<bool>(
                name: "ATIVO",
                table: "TAB_ORFANATO",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "ABRE_AS",
                table: "TAB_ORFANATO",
                type: "VARCHAR(5)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FECHA_AS",
                table: "TAB_ORFANATO",
                type: "VARCHAR(5)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ABRE_AS",
                table: "TAB_ORFANATO");

            migrationBuilder.DropColumn(
                name: "FECHA_AS",
                table: "TAB_ORFANATO");

            migrationBuilder.AlterColumn<bool>(
                name: "ATIVO",
                table: "TAB_ORFANATO",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "HORA_DE_ABERTURA",
                table: "TAB_ORFANATO",
                type: "VARCHAR(50)",
                nullable: false,
                defaultValue: "");
        }
    }
}
