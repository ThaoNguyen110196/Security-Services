using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class province : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Provinces_Matp",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Districts_DistrictId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Provinces_ProvinceId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DistrictId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ProvinceId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Districts",
                table: "Districts");

            migrationBuilder.DropIndex(
                name: "IX_Districts_Matp",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "Matp",
                table: "Provinces");

            migrationBuilder.DropColumn(
                name: "Maqh",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "Matp",
                table: "Districts");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Provinces",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ProvinceId",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DistrictId",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId1",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId1",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Districts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "Districts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Districts",
                table: "Districts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DistrictId1",
                table: "Employees",
                column: "DistrictId1");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ProvinceId1",
                table: "Employees",
                column: "ProvinceId1");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_ProvinceId",
                table: "Districts",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Provinces_ProvinceId",
                table: "Districts",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Districts_DistrictId1",
                table: "Employees",
                column: "DistrictId1",
                principalTable: "Districts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Provinces_ProvinceId1",
                table: "Employees",
                column: "ProvinceId1",
                principalTable: "Provinces",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Provinces_ProvinceId",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Districts_DistrictId1",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Provinces_ProvinceId1",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DistrictId1",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ProvinceId1",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Districts",
                table: "Districts");

            migrationBuilder.DropIndex(
                name: "IX_Districts_ProvinceId",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Provinces");

            migrationBuilder.DropColumn(
                name: "DistrictId1",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ProvinceId1",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "Districts");

            migrationBuilder.AddColumn<string>(
                name: "Matp",
                table: "Provinces",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProvinceId",
                table: "Employees",
                type: "nvarchar(5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DistrictId",
                table: "Employees",
                type: "nvarchar(5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Maqh",
                table: "Districts",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Matp",
                table: "Districts",
                type: "nvarchar(5)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces",
                column: "Matp");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Districts",
                table: "Districts",
                column: "Maqh");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DistrictId",
                table: "Employees",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ProvinceId",
                table: "Employees",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_Matp",
                table: "Districts",
                column: "Matp");

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Provinces_Matp",
                table: "Districts",
                column: "Matp",
                principalTable: "Provinces",
                principalColumn: "Matp",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Districts_DistrictId",
                table: "Employees",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Maqh");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Provinces_ProvinceId",
                table: "Employees",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Matp");
        }
    }
}
