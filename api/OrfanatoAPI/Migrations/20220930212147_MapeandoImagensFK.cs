using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrfanatoAPI.Migrations
{
    public partial class MapeandoImagensFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TAB_IMAGENS_TAB_ORFANATO_OrfanatoId",
                table: "TAB_IMAGENS");

            migrationBuilder.RenameColumn(
                name: "OrfanatoId",
                table: "TAB_IMAGENS",
                newName: "ORFANATO_ID");

            migrationBuilder.RenameIndex(
                name: "IX_TAB_IMAGENS_OrfanatoId",
                table: "TAB_IMAGENS",
                newName: "IX_TAB_IMAGENS_ORFANATO_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TAB_IMAGENS_TAB_ORFANATO_ORFANATO_ID",
                table: "TAB_IMAGENS",
                column: "ORFANATO_ID",
                principalTable: "TAB_ORFANATO",
                principalColumn: "ID_ORFANATO",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TAB_IMAGENS_TAB_ORFANATO_ORFANATO_ID",
                table: "TAB_IMAGENS");

            migrationBuilder.RenameColumn(
                name: "ORFANATO_ID",
                table: "TAB_IMAGENS",
                newName: "OrfanatoId");

            migrationBuilder.RenameIndex(
                name: "IX_TAB_IMAGENS_ORFANATO_ID",
                table: "TAB_IMAGENS",
                newName: "IX_TAB_IMAGENS_OrfanatoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TAB_IMAGENS_TAB_ORFANATO_OrfanatoId",
                table: "TAB_IMAGENS",
                column: "OrfanatoId",
                principalTable: "TAB_ORFANATO",
                principalColumn: "ID_ORFANATO",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
