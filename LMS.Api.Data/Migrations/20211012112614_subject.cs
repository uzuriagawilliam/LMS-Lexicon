using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Api.Data.Migrations
{
    public partial class subject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Literature");

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.SubjectId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Literature_SubjectId",
                table: "Literature",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Literature_Subject_SubjectId",
                table: "Literature",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Literature_Subject_SubjectId",
                table: "Literature");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Literature_SubjectId",
                table: "Literature");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Literature",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
