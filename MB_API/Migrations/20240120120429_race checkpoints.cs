using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MB_API.Migrations
{
    /// <inheritdoc />
    public partial class racecheckpoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RaceCheckPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RaceId = table.Column<int>(type: "integer", nullable: false),
                    CheckPointId = table.Column<int>(type: "integer", nullable: false),
                    Lap = table.Column<int>(type: "integer", nullable: false)
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
                        name: "FK_RaceCheckPoints_Events_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Events",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RaceCheckPoints");
        }
    }
}
