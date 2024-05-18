using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackEnd_PGI.Migrations
{
    /// <inheritdoc />
    public partial class AddIocs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "ioc");

            migrationBuilder.AddColumn<int>(
                name: "AssetID",
                table: "ioc",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoIocId",
                table: "ioc",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TipoIOCs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Icono = table.Column<string>(type: "text", nullable: false),
                    BuscarEn = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoIOCs", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ioc_AssetID",
                table: "ioc",
                column: "AssetID");

            migrationBuilder.CreateIndex(
                name: "IX_ioc_TipoIocId",
                table: "ioc",
                column: "TipoIocId");

            migrationBuilder.CreateIndex(
                name: "IX_asset_TipoAssetID",
                table: "asset",
                column: "TipoAssetID");

            migrationBuilder.AddForeignKey(
                name: "FK_asset_TipoAssets_TipoAssetID",
                table: "asset",
                column: "TipoAssetID",
                principalTable: "TipoAssets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ioc_TipoIOCs_TipoIocId",
                table: "ioc",
                column: "TipoIocId",
                principalTable: "TipoIOCs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ioc_asset_AssetID",
                table: "ioc",
                column: "AssetID",
                principalTable: "asset",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_asset_TipoAssets_TipoAssetID",
                table: "asset");

            migrationBuilder.DropForeignKey(
                name: "FK_ioc_TipoIOCs_TipoIocId",
                table: "ioc");

            migrationBuilder.DropForeignKey(
                name: "FK_ioc_asset_AssetID",
                table: "ioc");

            migrationBuilder.DropTable(
                name: "TipoIOCs");

            migrationBuilder.DropIndex(
                name: "IX_ioc_AssetID",
                table: "ioc");

            migrationBuilder.DropIndex(
                name: "IX_ioc_TipoIocId",
                table: "ioc");

            migrationBuilder.DropIndex(
                name: "IX_asset_TipoAssetID",
                table: "asset");

            migrationBuilder.DropColumn(
                name: "AssetID",
                table: "ioc");

            migrationBuilder.DropColumn(
                name: "TipoIocId",
                table: "ioc");

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "ioc",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
