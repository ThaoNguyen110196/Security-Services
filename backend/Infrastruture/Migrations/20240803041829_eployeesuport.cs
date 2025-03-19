using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class eployeesuport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSupports_Employees_EmployeeId",
                table: "EmployeeSupports");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSupports_Employees_EmployeeId",
                table: "EmployeeSupports",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSupports_Employees_EmployeeId",
                table: "EmployeeSupports");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSupports_Employees_EmployeeId",
                table: "EmployeeSupports",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
