using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentroDeAdopcion_LaEsperanza.Migrations
{
    /// <inheritdoc />
    public partial class AddEstadoToAdopcion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Adopciones",
                type: "nvarchar(max)",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Adopciones");
        }
    }
}
