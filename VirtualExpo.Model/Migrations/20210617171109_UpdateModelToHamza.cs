using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtualExpo.Model.Migrations
{
    public partial class UpdateModelToHamza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exhibitor");

            migrationBuilder.DropColumn(
                name: "SessionDateTime",
                table: "MediaLinks");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Exhibition",
                newName: "Organizer_User_Id");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Education",
                newName: "Attendee_User_Id");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExhibitionId",
                table: "RequestOrganizer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "RequestOrganizer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExhibitorId",
                table: "MediaLinks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExhibitorDescription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exibition_id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Offer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExhibitorDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExhibitorDescription_Exhibition_Exibition_id",
                        column: x => x.Exibition_id,
                        principalTable: "Exhibition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExhibitorDescription_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestOrganizer_ExhibitionId",
                table: "RequestOrganizer",
                column: "ExhibitionId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaLinks_ExhibitorId",
                table: "MediaLinks",
                column: "ExhibitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Exhibition_Organizer_User_Id",
                table: "Exhibition",
                column: "Organizer_User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Education_Attendee_User_Id",
                table: "Education",
                column: "Attendee_User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ExhibitorDescription_Exibition_id",
                table: "ExhibitorDescription",
                column: "Exibition_id");

            migrationBuilder.CreateIndex(
                name: "IX_ExhibitorDescription_UserId",
                table: "ExhibitorDescription",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_User_Attendee_User_Id",
                table: "Education",
                column: "Attendee_User_Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exhibition_User_Organizer_User_Id",
                table: "Exhibition",
                column: "Organizer_User_Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaLinks_ExhibitorDescription_ExhibitorId",
                table: "MediaLinks",
                column: "ExhibitorId",
                principalTable: "ExhibitorDescription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestOrganizer_Exhibition_ExhibitionId",
                table: "RequestOrganizer",
                column: "ExhibitionId",
                principalTable: "Exhibition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_User_Attendee_User_Id",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Exhibition_User_Organizer_User_Id",
                table: "Exhibition");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaLinks_ExhibitorDescription_ExhibitorId",
                table: "MediaLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestOrganizer_Exhibition_ExhibitionId",
                table: "RequestOrganizer");

            migrationBuilder.DropTable(
                name: "ExhibitorDescription");

            migrationBuilder.DropIndex(
                name: "IX_RequestOrganizer_ExhibitionId",
                table: "RequestOrganizer");

            migrationBuilder.DropIndex(
                name: "IX_MediaLinks_ExhibitorId",
                table: "MediaLinks");

            migrationBuilder.DropIndex(
                name: "IX_Exhibition_Organizer_User_Id",
                table: "Exhibition");

            migrationBuilder.DropIndex(
                name: "IX_Education_Attendee_User_Id",
                table: "Education");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ExhibitionId",
                table: "RequestOrganizer");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "RequestOrganizer");

            migrationBuilder.DropColumn(
                name: "ExhibitorId",
                table: "MediaLinks");

            migrationBuilder.RenameColumn(
                name: "Organizer_User_Id",
                table: "Exhibition",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "Attendee_User_Id",
                table: "Education",
                newName: "User_Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "SessionDateTime",
                table: "MediaLinks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Exhibitor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Exibition_id = table.Column<int>(type: "int", nullable: false),
                    Moto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Offer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exhibitor", x => x.Id);
                });
        }
    }
}
