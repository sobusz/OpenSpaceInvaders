using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenSpaceInvaders.Data.Migrations
{
    public partial class ChangedBookingModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingModel_CustomerModel_CustomerId",
                table: "BookingModel");

            migrationBuilder.DropTable(
                name: "CustomerModel");

            migrationBuilder.DropIndex(
                name: "IX_BookingModel_CustomerId",
                table: "BookingModel");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "BookingModel",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BookingModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BookingModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "BookingModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "BookingModel",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "BookingModel");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BookingModel");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "BookingModel");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "BookingModel");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "BookingModel",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingModel_CustomerId",
                table: "BookingModel",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingModel_CustomerModel_CustomerId",
                table: "BookingModel",
                column: "CustomerId",
                principalTable: "CustomerModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
