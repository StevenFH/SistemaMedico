using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SistemaMedico.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, "Medicamentos para aliviar el dolor", "Analgésicos" },
                    { 2, "Fármacos para tratar infecciones bacterianas", "Antibióticos" },
                    { 3, "Insumos y herramientas de uso diario", "Equipamiento Básico" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Cantidad", "CategoriaId", "Descripcion", "Nombre", "Precio" },
                values: new object[,]
                {
                    { 1, 200, 1, "Caja x 100 tabletas", "Paracetamol 500mg", 5.50m },
                    { 2, 150, 1, "Caja x 50 cápsulas", "Ibuprofeno 400mg", 8.75m },
                    { 3, 80, 2, "Caja x 20 cápsulas", "Amoxicilina 500mg", 12.00m },
                    { 4, 50, 2, "Caja x 5 tabletas", "Azitromicina 250mg", 15.30m },
                    { 5, 30, 3, "Medición rápida con alarma", "Termómetro Digital", 8.00m },
                    { 6, 500, 3, "Caja x 50 unidades", "Mascarillas KN95", 20.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
