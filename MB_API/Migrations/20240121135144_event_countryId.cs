using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MB_API.Migrations
{
    /// <inheritdoc />
    public partial class event_countryId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Events",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Events_CountryId",
                table: "Events",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Countries_CountryId",
                table: "Events",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Countries_CountryId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_CountryId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Events");
        }
    }
}
