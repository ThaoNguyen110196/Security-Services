using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class customerdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSupports_Customers_CustomerId",
                table: "EmployeeSupports");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeSupports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSupports_Customers_CustomerId",
                table: "EmployeeSupports",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSupports_Customers_CustomerId",
                table: "EmployeeSupports");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EmployeeSupports");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSupports_Customers_CustomerId",
                table: "EmployeeSupports",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
