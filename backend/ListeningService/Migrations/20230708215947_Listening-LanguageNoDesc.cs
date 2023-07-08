using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ListeningService.Migrations
{
    public partial class ListeningLanguageNoDesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "T_Index");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "T_Index",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
