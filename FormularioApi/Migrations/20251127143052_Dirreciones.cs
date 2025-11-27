using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FormularioApi.Migrations
{
    /// <inheritdoc />
    public partial class Dirreciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dirreciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Ubicacion = table.Column<string>(type: "text", nullable: false),
                    Ciudad = table.Column<string>(type: "text", nullable: false),
                    Provicia = table.Column<string>(type: "text", nullable: false),
                    CodigoPostal = table.Column<string>(type: "text", nullable: false),
                    Pais = table.Column<string>(type: "text", nullable: false),
                    PersonaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dirreciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dirreciones_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dirreciones_PersonaId",
                table: "Dirreciones",
                column: "PersonaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dirreciones");
        }
    }
}
