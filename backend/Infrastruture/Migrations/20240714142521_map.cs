using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class map : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Tows_TowId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Tows");

            migrationBuilder.DropTable(
                name: "Citys");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TowId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "TowId",
                table: "Employees",
                newName: "XaPhuongThiTranId");

            migrationBuilder.AddColumn<int>(
                name: "QuanHuyenId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuanHuyenMaqh",
                table: "Employees",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TinhThanhPhoId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhThanhPhoMatp",
                table: "Employees",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XaPhuongThiTranXaid",
                table: "Employees",
                type: "nvarchar(5)",
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
                    NameQuanHuyen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Matp = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    NameXaPhuong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Maqh = table.Column<string>(type: "nvarchar(5)", nullable: false)
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
                name: "IX_Employees_QuanHuyenMaqh",
                table: "Employees",
                column: "QuanHuyenMaqh");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TinhThanhPhoMatp",
                table: "Employees",
                column: "TinhThanhPhoMatp");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_XaPhuongThiTranXaid",
                table: "Employees",
                column: "XaPhuongThiTranXaid");

            migrationBuilder.CreateIndex(
                name: "IX_QuanHuyens_Matp",
                table: "QuanHuyens",
                column: "Matp");

            migrationBuilder.CreateIndex(
                name: "IX_XaPhuongThiTrans_Maqh",
                table: "XaPhuongThiTrans",
                column: "Maqh");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_XaPhuongThiTrans_XaPhuongThiTranXaid",
                table: "Employees",
                column: "XaPhuongThiTranXaid",
                principalTable: "XaPhuongThiTrans",
                principalColumn: "Xaid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_QuanHuyens_QuanHuyenMaqh",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_TinhThanhPhos_TinhThanhPhoMatp",
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
                name: "IX_Employees_QuanHuyenMaqh",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TinhThanhPhoMatp",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_XaPhuongThiTranXaid",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "QuanHuyenId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "QuanHuyenMaqh",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TinhThanhPhoId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TinhThanhPhoMatp",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "XaPhuongThiTranXaid",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "XaPhuongThiTranId",
                table: "Employees",
                newName: "TowId");

            migrationBuilder.CreateTable(
                name: "Citys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tows_Citys_CityId",
                        column: x => x.CityId,
                        principalTable: "Citys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TowId",
                table: "Employees",
                column: "TowId");

            migrationBuilder.CreateIndex(
                name: "IX_Tows_CityId",
                table: "Tows",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Tows_TowId",
                table: "Employees",
                column: "TowId",
                principalTable: "Tows",
                principalColumn: "Id");
        }
    }
}
