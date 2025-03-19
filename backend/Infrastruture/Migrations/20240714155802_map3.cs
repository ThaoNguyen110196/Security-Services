using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class map3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_QuanHuyens_QuanHuyenMaqh",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_TinhThanhPhos_TinhThanhPhoMatp",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_QuanHuyenMaqh",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TinhThanhPhoMatp",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "QuanHuyenMaqh",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TinhThanhPhoMatp",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "TinhThanhPhoId",
                table: "Employees",
                type: "nvarchar(5)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QuanHuyenId",
                table: "Employees",
                type: "nvarchar(5)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_QuanHuyenId",
                table: "Employees",
                column: "QuanHuyenId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TinhThanhPhoId",
                table: "Employees",
                column: "TinhThanhPhoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_QuanHuyens_QuanHuyenId",
                table: "Employees",
                column: "QuanHuyenId",
                principalTable: "QuanHuyens",
                principalColumn: "Maqh");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_TinhThanhPhos_TinhThanhPhoId",
                table: "Employees",
                column: "TinhThanhPhoId",
                principalTable: "TinhThanhPhos",
                principalColumn: "Matp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_QuanHuyens_QuanHuyenId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_TinhThanhPhos_TinhThanhPhoId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_QuanHuyenId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TinhThanhPhoId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "TinhThanhPhoId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuanHuyenId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuanHuyenMaqh",
                table: "Employees",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhThanhPhoMatp",
                table: "Employees",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_QuanHuyenMaqh",
                table: "Employees",
                column: "QuanHuyenMaqh");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TinhThanhPhoMatp",
                table: "Employees",
                column: "TinhThanhPhoMatp");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_QuanHuyens_QuanHuyenMaqh",
                table: "Employees",
                column: "QuanHuyenMaqh",
                principalTable: "QuanHuyens",
                principalColumn: "Maqh");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_TinhThanhPhos_TinhThanhPhoMatp",
                table: "Employees",
                column: "TinhThanhPhoMatp",
                principalTable: "TinhThanhPhos",
                principalColumn: "Matp");
        }
    }
}
