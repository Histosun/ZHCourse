using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ListeningService.Migrations
{
    public partial class ListeningIndexUri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "coverPicUri",
                table: "T_Index",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "coverPicUri",
                table: "T_Index");
        }
    }
}
