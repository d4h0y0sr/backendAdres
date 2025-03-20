using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace adquisicionAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adquisiciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Presupuesto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoDeBien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaAdquisicion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Proveedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Documentacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adquisiciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Historial",
                columns: table => new
                {
                    IdHistorial = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAdquisicion = table.Column<int>(type: "int", nullable: false),
                    Operacion = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FechaCambio = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Usuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "SUSER_NAME()"),
                    Cambios = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historial", x => x.IdHistorial);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adquisiciones");

            migrationBuilder.DropTable(
                name: "Historial");
        }
    }
}
