using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MB_API.Migrations
{
    /// <inheritdoc />
    public partial class checkpointupd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckPoints_Events_EventId",
                table: "CheckPoints");

            migrationBuilder.DropIndex(
                name: "IX_CheckPoints_EventId",
                table: "CheckPoints");

            migrationBuilder.RenameColumn(
                name: "CheckpointName",
                table: "CheckPoints",
                newName: "CheckPointName");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "CheckPoints",
                newName: "TrackId");

            migrationBuilder.AddColumn<int>(
                name: "StageNumber",
                table: "Results",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CheckPointTypeId",
                table: "CheckPoints",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StageNumber",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "CheckPointTypeId",
                table: "CheckPoints");

            migrationBuilder.RenameColumn(
                name: "CheckPointName",
                table: "CheckPoints",
                newName: "CheckpointName");

            migrationBuilder.RenameColumn(
                name: "TrackId",
                table: "CheckPoints",
                newName: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckPoints_EventId",
                table: "CheckPoints",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckPoints_Events_EventId",
                table: "CheckPoints",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
