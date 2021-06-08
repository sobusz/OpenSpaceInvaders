using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenSpaceInvaders.Data.Migrations
{
    public partial class AddedBlobsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BlobModel",
                columns: table => new
                {
                    media_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<string>(nullable: true),
                    media_file_name = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    media_file_type = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    media_url = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    DateTimeUploaded = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserMedia", x => x.media_id);
                    table.ForeignKey(
                        name: "UserMedia_fk_User",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlobModel_user_id",
                table: "BlobModel",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlobModel");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
