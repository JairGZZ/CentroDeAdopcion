using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentroDeAdopcion_LaEsperanza.Migrations
{
    /// <inheritdoc />
    public partial class BorrarEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Adopciones");

         
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
