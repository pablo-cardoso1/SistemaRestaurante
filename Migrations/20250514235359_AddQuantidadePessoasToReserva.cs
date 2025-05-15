using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaRestaurante.Migrations
{
    /// <inheritdoc />
    public partial class AddQuantidadePessoasToReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantidadePessoas",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantidadePessoas",
                table: "Reservas");
        }
    }
}
