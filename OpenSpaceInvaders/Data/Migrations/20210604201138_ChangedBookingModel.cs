using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenSpaceInvaders.Data.Migrations
{
    public partial class ChangedBookingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "BookingModel");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BookingModel");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "BookingModel");

            migrationBuilder.CreateTable(
                name: "BookingDate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(nullable: false),
                    BookingModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingDate_BookingModel_BookingModelId",
                        column: x => x.BookingModelId,
                        principalTable: "BookingModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDate_BookingModelId",
                table: "BookingDate",
                column: "BookingModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "BookingModel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BookingModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "BookingModel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
