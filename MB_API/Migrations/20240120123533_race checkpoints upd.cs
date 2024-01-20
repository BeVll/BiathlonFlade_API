using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MB_API.Migrations
{
    /// <inheritdoc />
    public partial class racecheckpointsupd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RaceCheckPoints",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "RaceCheckPoints",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "RaceCheckPoints");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "RaceCheckPoints");
        }
    }
}
