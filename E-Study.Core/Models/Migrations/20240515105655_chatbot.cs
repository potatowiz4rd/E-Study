using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Study.Core.Migrations
{
    public partial class chatbot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamCourses_Events_EventId",
                table: "ExamCourses");

            migrationBuilder.DropIndex(
                name: "IX_ExamCourses_EventId",
                table: "ExamCourses");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "ExamCourses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventId",
                table: "ExamCourses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamCourses_EventId",
                table: "ExamCourses",
                column: "EventId",
                unique: true,
                filter: "[EventId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamCourses_Events_EventId",
                table: "ExamCourses",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
