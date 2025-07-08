using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticaFinalApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           // Eliminar índices si existen
            migrationBuilder.Sql("DROP INDEX IF EXISTS IX_Equipos_OperacionId;");
            migrationBuilder.Sql("CREATE INDEX IX_Equipos_OperacionId ON Equipos (OperacionId);");

            migrationBuilder.Sql("DROP INDEX IF EXISTS IX_Agentes_EquipoId;");
            migrationBuilder.Sql("CREATE INDEX IX_Agentes_EquipoId ON Agentes (EquipoId);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar índices
            migrationBuilder.Sql("DROP INDEX IF EXISTS IX_Equipos_OperacionId;");
            migrationBuilder.Sql("DROP INDEX IF EXISTS IX_Agentes_EquipoId;");
        }
    }
}
