using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class vacation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departners_DepartnentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_EmployeeId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_GeneralDepartment_GenralDepartmentId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DepartnentId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_GenralDepartmentId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DepartnentId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "GenralDepartmentId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "GeneralDepartmentId",
                table: "Departners",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartnentId",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OvertimeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvertimeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SanctionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanctionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VacastionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacastionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OverTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OvertimeTypeId = table.Column<int>(type: "int", nullable: true),
                    OvertimTypeId = table.Column<int>(type: "int", nullable: false),
                    CivilId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OverTime_OvertimeTypes_OvertimeTypeId",
                        column: x => x.OvertimeTypeId,
                        principalTable: "OvertimeTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sanctions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Punishment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PunishmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SanctionTypeId = table.Column<int>(type: "int", nullable: true),
                    CivilId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sanctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sanctions_SanctionTypes_SanctionTypeId",
                        column: x => x.SanctionTypeId,
                        principalTable: "SanctionTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vacations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDtae = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfDays = table.Column<int>(type: "int", nullable: false),
                    VacationTypeId = table.Column<int>(type: "int", nullable: false),
                    CivilId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacations_VacastionTypes_VacationTypeId",
                        column: x => x.VacationTypeId,
                        principalTable: "VacastionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departners_GeneralDepartmentId",
                table: "Departners",
                column: "GeneralDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_DepartnentId",
                table: "Branches",
                column: "DepartnentId");

            migrationBuilder.CreateIndex(
                name: "IX_OverTime_OvertimeTypeId",
                table: "OverTime",
                column: "OvertimeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sanctions_SanctionTypeId",
                table: "Sanctions",
                column: "SanctionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_VacationTypeId",
                table: "Vacations",
                column: "VacationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Departners_DepartnentId",
                table: "Branches",
                column: "DepartnentId",
                principalTable: "Departners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departners_GeneralDepartment_GeneralDepartmentId",
                table: "Departners",
                column: "GeneralDepartmentId",
                principalTable: "GeneralDepartment",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Departners_DepartnentId",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Departners_GeneralDepartment_GeneralDepartmentId",
                table: "Departners");

            migrationBuilder.DropTable(
                name: "OverTime");

            migrationBuilder.DropTable(
                name: "Sanctions");

            migrationBuilder.DropTable(
                name: "Vacations");

            migrationBuilder.DropTable(
                name: "OvertimeTypes");

            migrationBuilder.DropTable(
                name: "SanctionTypes");

            migrationBuilder.DropTable(
                name: "VacastionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Departners_GeneralDepartmentId",
                table: "Departners");

            migrationBuilder.DropIndex(
                name: "IX_Branches_DepartnentId",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "GeneralDepartmentId",
                table: "Departners");

            migrationBuilder.DropColumn(
                name: "DepartnentId",
                table: "Branches");

            migrationBuilder.AddColumn<int>(
                name: "DepartnentId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GenralDepartmentId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartnentId",
                table: "Employees",
                column: "DepartnentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeId",
                table: "Employees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GenralDepartmentId",
                table: "Employees",
                column: "GenralDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departners_DepartnentId",
                table: "Employees",
                column: "DepartnentId",
                principalTable: "Departners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_EmployeeId",
                table: "Employees",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_GeneralDepartment_GenralDepartmentId",
                table: "Employees",
                column: "GenralDepartmentId",
                principalTable: "GeneralDepartment",
                principalColumn: "Id");
        }
    }
}
