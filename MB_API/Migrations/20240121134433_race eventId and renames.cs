using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MB_API.Migrations
{
    /// <inheritdoc />
    public partial class raceeventIdandrenames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Races_RaceTypes_EventTypeId",
                table: "Races");

            migrationBuilder.RenameColumn(
                name: "TeamEvent",
                table: "Races",
                newName: "TeamRace");

            migrationBuilder.RenameColumn(
                name: "EventTypeId",
                table: "Races",
                newName: "RaceTypeId");

            migrationBuilder.RenameColumn(
                name: "EventName",
                table: "Races",
                newName: "RaceName");

            migrationBuilder.RenameColumn(
                name: "EventDate",
                table: "Races",
                newName: "RaceDate");

            migrationBuilder.RenameIndex(
                name: "IX_Races_EventTypeId",
                table: "Races",
                newName: "IX_Races_RaceTypeId");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Races",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Races_EventId",
                table: "Races",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Events_EventId",
                table: "Races",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Races_RaceTypes_RaceTypeId",
                table: "Races",
                column: "RaceTypeId",
                principalTable: "RaceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Races_Events_EventId",
                table: "Races");

            migrationBuilder.DropForeignKey(
                name: "FK_Races_RaceTypes_RaceTypeId",
                table: "Races");

            migrationBuilder.DropIndex(
                name: "IX_Races_EventId",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Races");

            migrationBuilder.RenameColumn(
                name: "TeamRace",
                table: "Races",
                newName: "TeamEvent");

            migrationBuilder.RenameColumn(
                name: "RaceTypeId",
                table: "Races",
                newName: "EventTypeId");

            migrationBuilder.RenameColumn(
                name: "RaceName",
                table: "Races",
                newName: "EventName");

            migrationBuilder.RenameColumn(
                name: "RaceDate",
                table: "Races",
                newName: "EventDate");

            migrationBuilder.RenameIndex(
                name: "IX_Races_RaceTypeId",
                table: "Races",
                newName: "IX_Races_EventTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Races_RaceTypes_EventTypeId",
                table: "Races",
                column: "EventTypeId",
                principalTable: "RaceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
