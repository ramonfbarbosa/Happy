using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrfanatoAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TAB_ORFANATO",
                columns: table => new
                {
                    ID_ORFANATO = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    LATITUDE = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    LONGITUDE = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    SOBRE = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    INSTRUCOES = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    HORA_DE_ABERTURA = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    ABERTO_FIM_DE_SEMANA = table.Column<bool>(type: "bit", nullable: false),
                    ATIVO = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_ORFANATO", x => x.ID_ORFANATO);
                });

            migrationBuilder.CreateTable(
                name: "TAB_IMAGENS",
                columns: table => new
                {
                    ID_IMAGENS = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IMAGEM_URL = table.Column<string>(type: "VARCHAR(MAX)", nullable: false),
                    OrfanatoId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_IMAGENS", x => x.ID_IMAGENS);
                    table.ForeignKey(
                        name: "FK_TAB_IMAGENS_TAB_ORFANATO_OrfanatoId",
                        column: x => x.OrfanatoId,
                        principalTable: "TAB_ORFANATO",
                        principalColumn: "ID_ORFANATO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TAB_IMAGENS_OrfanatoId",
                table: "TAB_IMAGENS",
                column: "OrfanatoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TAB_IMAGENS");

            migrationBuilder.DropTable(
                name: "TAB_ORFANATO");
        }
    }
}
