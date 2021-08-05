using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtualExpo.Model.Migrations
{
    public partial class updateexhibimodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Download",
                table: "MediaLinks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DownloadDescription",
                table: "MediaLinks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SocialNetwork",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instagram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SnapChat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Linkdin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exhibitor_Id = table.Column<int>(type: "int", nullable: false),
                    ExhibitorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialNetwork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialNetwork_ExhibitorDescription_ExhibitorId",
                        column: x => x.ExhibitorId,
                        principalTable: "ExhibitorDescription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocialNetwork_ExhibitorId",
                table: "SocialNetwork",
                column: "ExhibitorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialNetwork");

            migrationBuilder.DropColumn(
                name: "Download",
                table: "MediaLinks");

            migrationBuilder.DropColumn(
                name: "DownloadDescription",
                table: "MediaLinks");
        }
    }
}
