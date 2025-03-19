using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class jobposition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "JobPositions");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "JobPositions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "JobPositions");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "JobPositions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
