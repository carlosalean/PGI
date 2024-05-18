using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd_PGI.Migrations
{
    /// <inheritdoc />
    public partial class RelacionCasoMaquina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maquinas_caso_CasoID",
                table: "Maquinas");

            migrationBuilder.AlterColumn<int>(
                name: "CasoID",
                table: "Maquinas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Maquinas_caso_CasoID",
                table: "Maquinas",
                column: "CasoID",
                principalTable: "caso",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maquinas_caso_CasoID",
                table: "Maquinas");

            migrationBuilder.AlterColumn<int>(
                name: "CasoID",
                table: "Maquinas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Maquinas_caso_CasoID",
                table: "Maquinas",
                column: "CasoID",
                principalTable: "caso",
                principalColumn: "ID");
        }
    }
}
