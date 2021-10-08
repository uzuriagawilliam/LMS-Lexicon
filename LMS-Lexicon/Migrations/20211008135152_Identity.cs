using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS_Lexicon.Migrations
{
    public partial class Identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeOfRegistrationj",
                table: "AspNetUsers",
                newName: "TimeOfRegistration");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeOfRegistration",
                table: "AspNetUsers",
                newName: "TimeOfRegistrationj");
        }
    }
}
