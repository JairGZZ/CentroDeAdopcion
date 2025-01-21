using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentroDeAdopcion_LaEsperanza.Migrations
{
    /// <inheritdoc />
    public partial class AgregarEstadoOTRAVEZ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Mascotas",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                defaultValue: "disponible");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Mascotas");

        }
    }
}
