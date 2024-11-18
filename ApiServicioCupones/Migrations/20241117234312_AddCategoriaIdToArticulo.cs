using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiServicioCupones.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriaIdToArticulo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Articulos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoriaModelId_Categoria",
                table: "Articulos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_CategoriaModelId_Categoria",
                table: "Articulos",
                column: "CategoriaModelId_Categoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Categorias_CategoriaModelId_Categoria",
                table: "Articulos",
                column: "CategoriaModelId_Categoria",
                principalTable: "Categorias",
                principalColumn: "Id_Categoria");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Categorias_CategoriaModelId_Categoria",
                table: "Articulos");

            migrationBuilder.DropIndex(
                name: "IX_Articulos_CategoriaModelId_Categoria",
                table: "Articulos");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Articulos");

            migrationBuilder.DropColumn(
                name: "CategoriaModelId_Categoria",
                table: "Articulos");
        }
    }
}
