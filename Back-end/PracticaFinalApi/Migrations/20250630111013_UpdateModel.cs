using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticaFinalApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Equipos_OperacionId",
                table: "Equipos",
                column: "OperacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Agentes_EquipoId",
                table: "Agentes",
                column: "EquipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agentes_Equipos_EquipoId",
                table: "Agentes",
                column: "EquipoId",
                principalTable: "Equipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Operaciones_OperacionId",
                table: "Equipos",
                column: "OperacionId",
                principalTable: "Operaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agentes_Equipos_EquipoId",
                table: "Agentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Operaciones_OperacionId",
                table: "Equipos");

            migrationBuilder.DropIndex(
                name: "IX_Equipos_OperacionId",
                table: "Equipos");

            migrationBuilder.DropIndex(
                name: "IX_Agentes_EquipoId",
                table: "Agentes");
        }
    }
}
