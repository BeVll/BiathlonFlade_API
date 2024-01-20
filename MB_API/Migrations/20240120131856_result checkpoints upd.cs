using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MB_API.Migrations
{
    /// <inheritdoc />
    public partial class resultcheckpointsupd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_CheckPoints_CheckpointId",
                table: "Results");

            migrationBuilder.RenameColumn(
                name: "CheckpointId",
                table: "Results",
                newName: "RaceCheckpointId");

            migrationBuilder.RenameIndex(
                name: "IX_Results_CheckpointId",
                table: "Results",
                newName: "IX_Results_RaceCheckpointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_RaceCheckPoints_RaceCheckpointId",
                table: "Results",
                column: "RaceCheckpointId",
                principalTable: "RaceCheckPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_RaceCheckPoints_RaceCheckpointId",
                table: "Results");

            migrationBuilder.RenameColumn(
                name: "RaceCheckpointId",
                table: "Results",
                newName: "CheckpointId");

            migrationBuilder.RenameIndex(
                name: "IX_Results_RaceCheckpointId",
                table: "Results",
                newName: "IX_Results_CheckpointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_CheckPoints_CheckpointId",
                table: "Results",
                column: "CheckpointId",
                principalTable: "CheckPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
