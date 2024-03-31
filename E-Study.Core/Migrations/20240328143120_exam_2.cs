using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Study.Core.Migrations
{
    public partial class exam_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Exams",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_AuthorId",
                table: "Exams",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Users_AuthorId",
                table: "Exams",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Users_AuthorId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_AuthorId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Exams");
        }
    }
}
