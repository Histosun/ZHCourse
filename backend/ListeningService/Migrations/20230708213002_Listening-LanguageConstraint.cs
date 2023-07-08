using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ListeningService.Migrations
{
    public partial class ListeningLanguageConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.RenameTable(
                name: "Language",
                newName: "T_Language");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_Language",
                table: "T_Language",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_T_Language_Name",
                table: "T_Language",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_T_Language",
                table: "T_Language");

            migrationBuilder.DropIndex(
                name: "IX_T_Language_Name",
                table: "T_Language");

            migrationBuilder.RenameTable(
                name: "T_Language",
                newName: "Language");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "Id");
        }
    }
}
