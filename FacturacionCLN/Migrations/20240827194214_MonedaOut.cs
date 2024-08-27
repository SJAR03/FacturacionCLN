using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacturacionCLN.Migrations
{
    /// <inheritdoc />
    public partial class MonedaOut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Moneda",
                table: "Facturas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Moneda",
                table: "Facturas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
