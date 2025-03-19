using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class Provice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_QuanHuyens_QuanHuyenId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_TinhThanhPhos_TinhThanhPhoId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_XaPhuongThiTrans_XaPhuongThiTranXaid",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "XaPhuongThiTrans");

            migrationBuilder.DropTable(
                name: "QuanHuyens");

            migrationBuilder.DropTable(
                name: "TinhThanhPhos");

            migrationBuilder.DropIndex(
                name: "IX_Employees_QuanHuyenId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "QuanHuyenId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "XaPhuongThiTranId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "XaPhuongThiTranXaid",
                table: "Employees",
                newName: "ProvinceId");

            migrationBuilder.RenameColumn(
                name: "TinhThanhPhoId",
                table: "Employees",
                newName: "DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_XaPhuongThiTranXaid",
                table: "Employees",
                newName: "IX_Employees_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_TinhThanhPhoId",
                table: "Employees",
                newName: "IX_Employees_DistrictId");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Matp = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    NameCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Matp);
                    table.ForeignKey(
                        name: "FK_Provinces_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Maqh = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    NameDistrict = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Matp = table.Column<string>(type: "nvarchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Maqh);
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_Matp",
                        column: x => x.Matp,
                        principalTable: "Provinces",
                        principalColumn: "Matp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CountryId",
                table: "Employees",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_Matp",
                table: "Districts",
                column: "Matp");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryId",
                table: "Provinces",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Countries_CountryId",
                table: "Employees",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Countries_CountryId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Districts_DistrictId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Provinces_ProvinceId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CountryId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "ProvinceId",
                table: "Employees",
                newName: "XaPhuongThiTranXaid");

            migrationBuilder.RenameColumn(
                name: "DistrictId",
                table: "Employees",
                newName: "TinhThanhPhoId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_ProvinceId",
                table: "Employees",
                newName: "IX_Employees_XaPhuongThiTranXaid");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_DistrictId",
                table: "Employees",
                newName: "IX_Employees_TinhThanhPhoId");

            migrationBuilder.AddColumn<string>(
                name: "QuanHuyenId",
                table: "Employees",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "XaPhuongThiTranId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TinhThanhPhos",
                columns: table => new
                {
                    Matp = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    NameCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinhThanhPhos", x => x.Matp);
                });

            migrationBuilder.CreateTable(
                name: "QuanHuyens",
                columns: table => new
                {
                    Maqh = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Matp = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    NameQuanHuyen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuanHuyens", x => x.Maqh);
                    table.ForeignKey(
                        name: "FK_QuanHuyens_TinhThanhPhos_Matp",
                        column: x => x.Matp,
                        principalTable: "TinhThanhPhos",
                        principalColumn: "Matp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "XaPhuongThiTrans",
                columns: table => new
                {
                    Xaid = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Maqh = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    NameXaPhuong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XaPhuongThiTrans", x => x.Xaid);
                    table.ForeignKey(
                        name: "FK_XaPhuongThiTrans_QuanHuyens_Maqh",
                        column: x => x.Maqh,
                        principalTable: "QuanHuyens",
                        principalColumn: "Maqh",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_QuanHuyenId",
                table: "Employees",
                column: "QuanHuyenId");

            migrationBuilder.CreateIndex(
                name: "IX_QuanHuyens_Matp",
                table: "QuanHuyens",
                column: "Matp");

            migrationBuilder.CreateIndex(
                name: "IX_XaPhuongThiTrans_Maqh",
                table: "XaPhuongThiTrans",
                column: "Maqh");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_XaPhuongThiTrans_XaPhuongThiTranXaid",
                table: "Employees",
                column: "XaPhuongThiTranXaid",
                principalTable: "XaPhuongThiTrans",
                principalColumn: "Xaid");
        }
    }
}
