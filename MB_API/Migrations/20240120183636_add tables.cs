using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MB_API.Migrations
{
    /// <inheritdoc />
    public partial class addtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventName = table.Column<string>(type: "text", nullable: false),
                    EventTypeId = table.Column<int>(type: "integer", nullable: false),
                    TeamEvent = table.Column<bool>(type: "boolean", nullable: false),
                    TrackId = table.Column<int>(type: "integer", nullable: false),
                    EventDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Races_RaceTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "RaceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Races_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceCheckPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RaceId = table.Column<int>(type: "integer", nullable: false),
                    CheckPointId = table.Column<int>(type: "integer", nullable: false),
                    Lap = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceCheckPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceCheckPoints_CheckPoints_CheckPointId",
                        column: x => x.CheckPointId,
                        principalTable: "CheckPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceCheckPoints_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RaceId = table.Column<int>(type: "integer", nullable: false),
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    RaceCheckpointId = table.Column<int>(type: "integer", nullable: false),
                    ResultValue = table.Column<long>(type: "bigint", nullable: true),
                    IsDNF = table.Column<bool>(type: "boolean", nullable: false),
                    StartNumber = table.Column<int>(type: "integer", nullable: false),
                    IsTeamResult = table.Column<bool>(type: "boolean", nullable: false),
                    IsFinish = table.Column<bool>(type: "boolean", nullable: false),
                    Lap = table.Column<int>(type: "integer", nullable: false),
                    TeamId = table.Column<int>(type: "integer", nullable: true),
                    StageNumber = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_RaceCheckPoints_RaceCheckpointId",
                        column: x => x.RaceCheckpointId,
                        principalTable: "RaceCheckPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RaceCheckPoints_CheckPointId",
                table: "RaceCheckPoints",
                column: "CheckPointId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceCheckPoints_RaceId",
                table: "RaceCheckPoints",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_EventTypeId",
                table: "Races",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_TrackId",
                table: "Races",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_PlayerId",
                table: "Results",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_RaceCheckpointId",
                table: "Results",
                column: "RaceCheckpointId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_RaceId",
                table: "Results",
                column: "RaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "RaceCheckPoints");

            migrationBuilder.DropTable(
                name: "Races");
        }
    }
}
