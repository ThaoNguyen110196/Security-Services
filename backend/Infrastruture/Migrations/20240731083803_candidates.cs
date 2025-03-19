using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class candidates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSupports_Customers_CustomerId",
                table: "EmployeeSupports");

            migrationBuilder.AddColumn<string>(
                name: "CvFilePath",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSupports_Customers_CustomerId",
                table: "EmployeeSupports",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSupports_Customers_CustomerId",
                table: "EmployeeSupports");

            migrationBuilder.DropColumn(
                name: "CvFilePath",
                table: "Candidates");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSupports_Customers_CustomerId",
                table: "EmployeeSupports",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
