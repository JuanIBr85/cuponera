using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiServicioCupones.Migrations
{
    /// <inheritdoc />
    public partial class testtest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Articulos_Id_Categoria",
                table: "Articulos",
                column: "Id_Categoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Categorias_Id_Categoria",
                table: "Articulos",
                column: "Id_Categoria",
                principalTable: "Categorias",
                principalColumn: "Id_Categoria",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Categorias_Id_Categoria",
                table: "Articulos");

            migrationBuilder.DropIndex(
                name: "IX_Articulos_Id_Categoria",
                table: "Articulos");
        }
    }
}
