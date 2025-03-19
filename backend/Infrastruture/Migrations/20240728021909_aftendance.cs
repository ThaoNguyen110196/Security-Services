using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class aftendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OvertimeTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "OverTime",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "OverTime",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "OverTime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OverTime_ApprovedById",
                table: "OverTime",
                column: "ApprovedById");

            migrationBuilder.AddForeignKey(
                name: "FK_OverTime_Managers_ApprovedById",
                table: "OverTime",
                column: "ApprovedById",
                principalTable: "Managers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OverTime_Managers_ApprovedById",
                table: "OverTime");

            migrationBuilder.DropIndex(
                name: "IX_OverTime_ApprovedById",
                table: "OverTime");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "OvertimeTypes");

            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "OverTime");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "OverTime");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "OverTime");
        }
    }
}
