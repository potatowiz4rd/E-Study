using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Study.Core.Migrations
{
    public partial class add_QuestionType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "QnAs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "QnAs");
        }
    }
}
