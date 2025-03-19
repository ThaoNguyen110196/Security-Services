using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class BranchUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Departners_DepartnentId",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_Branches_DepartnentId",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "DepartnentId",
                table: "Branches");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Branches");

            migrationBuilder.AddColumn<int>(
                name: "DepartnentId",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Branches_DepartnentId",
                table: "Branches",
                column: "DepartnentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Departners_DepartnentId",
                table: "Branches",
                column: "DepartnentId",
                principalTable: "Departners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
