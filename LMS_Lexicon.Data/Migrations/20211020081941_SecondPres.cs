using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS_Lexicon.Data.Migrations
{
    public partial class SecondPres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentClass_ActivityClass_ActivityId",
                table: "DocumentClass");

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                table: "DocumentClass",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentClass_ActivityClass_ActivityId",
                table: "DocumentClass",
                column: "ActivityId",
                principalTable: "ActivityClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentClass_ActivityClass_ActivityId",
                table: "DocumentClass");

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                table: "DocumentClass",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentClass_ActivityClass_ActivityId",
                table: "DocumentClass",
                column: "ActivityId",
                principalTable: "ActivityClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
