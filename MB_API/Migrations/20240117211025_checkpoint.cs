using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MB_API.Migrations
{
    /// <inheritdoc />
    public partial class checkpoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CheckPointName",
                table: "CheckPoints",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CheckPoints",
                newName: "CheckPointName");
        }
    }
}
