using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd_PGI.Migrations
{
    /// <inheritdoc />
    public partial class UsersMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_rol_RolID",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "RolID",
                table: "Usuarios",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_rol_RolID",
                table: "Usuarios",
                column: "RolID",
                principalTable: "rol",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_rol_RolID",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "RolID",
                table: "Usuarios",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_rol_RolID",
                table: "Usuarios",
                column: "RolID",
                principalTable: "rol",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
