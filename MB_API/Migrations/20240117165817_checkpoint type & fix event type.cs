using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MB_API.Migrations
{
    /// <inheritdoc />
    public partial class checkpointtypefixeventtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "CheckPointTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckPointTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId",
                table: "Events",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckPoints_CheckPointTypeId",
                table: "CheckPoints",
                column: "CheckPointTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckPoints_CheckPointTypes_CheckPointTypeId",
                table: "CheckPoints",
                column: "CheckPointTypeId",
                principalTable: "CheckPointTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventTypes_EventTypeId",
                table: "Events",
                column: "EventTypeId",
                principalTable: "EventTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckPoints_CheckPointTypes_CheckPointTypeId",
                table: "CheckPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventTypes_EventTypeId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "CheckPointTypes");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventTypeId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_CheckPoints_CheckPointTypeId",
                table: "CheckPoints");

            migrationBuilder.AlterColumn<string>(
                name: "EventTypeId",
                table: "Events",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "EventTypeId1",
                table: "Events",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId1",
                table: "Events",
                column: "EventTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventTypes_EventTypeId1",
                table: "Events",
                column: "EventTypeId1",
                principalTable: "EventTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
