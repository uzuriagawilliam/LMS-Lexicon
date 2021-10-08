using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS_Lexicon.Migrations
{
    public partial class UpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "ActivityClass");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "ActivityClass",
                type: "int",
                nullable: true);
        }
    }
}
