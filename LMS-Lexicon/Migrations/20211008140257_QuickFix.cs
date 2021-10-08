﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS_Lexicon.Migrations
{
    public partial class QuickFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CourseClass_CourseId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CourseClass_CourseId",
                table: "AspNetUsers",
                column: "CourseId",
                principalTable: "CourseClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CourseClass_CourseId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CourseClass_CourseId",
                table: "AspNetUsers",
                column: "CourseId",
                principalTable: "CourseClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
