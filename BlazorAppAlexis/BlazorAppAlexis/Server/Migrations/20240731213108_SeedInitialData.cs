using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorAppAlexis.Server.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Metas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PorcentajeCumplimiento = table.Column<double>(type: "float", nullable: false),
                    TareasCompletadas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MetaId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsImportante = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tareas_Metas_MetaId",
                        column: x => x.MetaId,
                        principalTable: "Metas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Metas",
                columns: new[] { "Id", "FechaCreada", "Nombre", "PorcentajeCumplimiento", "TareasCompletadas" },
                values: new object[] { 1, new DateTime(2024, 7, 31, 15, 31, 8, 379, DateTimeKind.Local).AddTicks(4401), "Meta 1", 0.0, 0 });

            migrationBuilder.InsertData(
                table: "Metas",
                columns: new[] { "Id", "FechaCreada", "Nombre", "PorcentajeCumplimiento", "TareasCompletadas" },
                values: new object[] { 2, new DateTime(2024, 7, 31, 15, 31, 8, 379, DateTimeKind.Local).AddTicks(4416), "Meta 2", 0.0, 0 });

            migrationBuilder.InsertData(
                table: "Tareas",
                columns: new[] { "Id", "EsImportante", "Estado", "FechaCreada", "MetaId", "Nombre" },
                values: new object[] { 1, false, "Abierta", new DateTime(2024, 7, 31, 15, 31, 8, 379, DateTimeKind.Local).AddTicks(4520), 1, "Tarea 1.1" });

            migrationBuilder.InsertData(
                table: "Tareas",
                columns: new[] { "Id", "EsImportante", "Estado", "FechaCreada", "MetaId", "Nombre" },
                values: new object[] { 2, true, "Abierta", new DateTime(2024, 7, 31, 15, 31, 8, 379, DateTimeKind.Local).AddTicks(4524), 1, "Tarea 1.2" });

            migrationBuilder.InsertData(
                table: "Tareas",
                columns: new[] { "Id", "EsImportante", "Estado", "FechaCreada", "MetaId", "Nombre" },
                values: new object[] { 3, false, "Abierta", new DateTime(2024, 7, 31, 15, 31, 8, 379, DateTimeKind.Local).AddTicks(4525), 2, "Tarea 2.1" });

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_MetaId",
                table: "Tareas",
                column: "MetaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.DropTable(
                name: "Metas");
        }
    }
}
