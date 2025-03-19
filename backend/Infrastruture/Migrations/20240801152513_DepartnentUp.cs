using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class DepartnentUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departners_Managers_ManagerId",
                table: "Departners");

            migrationBuilder.DropIndex(
                name: "IX_Departners_ManagerId",
                table: "Departners");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Departners");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Departners",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departners_ManagerId",
                table: "Departners",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departners_Managers_ManagerId",
                table: "Departners",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id");
        }
    }
}
