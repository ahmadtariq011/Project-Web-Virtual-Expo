using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtualExpo.Model.Migrations
{
    public partial class AddJunctionAttendeeExhibition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserExhibitionJunction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Exibition_id = table.Column<int>(type: "int", nullable: false),
                    Attendee_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExhibitionJunction", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserExhibitionJunction");
        }
    }
}
