using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MB_API.Migrations
{
    /// <inheritdoc />
    public partial class checkpointsvectors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "X1",
                table: "CheckPoints",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "X2",
                table: "CheckPoints",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Y1",
                table: "CheckPoints",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Y2",
                table: "CheckPoints",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Z1",
                table: "CheckPoints",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Z2",
                table: "CheckPoints",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "X1",
                table: "CheckPoints");

            migrationBuilder.DropColumn(
                name: "X2",
                table: "CheckPoints");

            migrationBuilder.DropColumn(
                name: "Y1",
                table: "CheckPoints");

            migrationBuilder.DropColumn(
                name: "Y2",
                table: "CheckPoints");

            migrationBuilder.DropColumn(
                name: "Z1",
                table: "CheckPoints");

            migrationBuilder.DropColumn(
                name: "Z2",
                table: "CheckPoints");
        }
    }
}
