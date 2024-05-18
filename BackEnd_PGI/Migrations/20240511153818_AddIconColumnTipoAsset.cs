using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd_PGI.Migrations
{
    /// <inheritdoc />
    public partial class AddIconColumnTipoAsset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icono",
                table: "TipoAssets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icono",
                table: "TipoAssets");
        }
    }
}
