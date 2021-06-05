using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenSpaceInvaders.Data.Migrations
{
    public partial class ChangedBookingModelDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "BookingModel",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "BookingModel",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "BookingModel");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "BookingModel");

            migrationBuilder.CreateTable(
                name: "BookingDate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingModelId = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false)
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
    }
}
