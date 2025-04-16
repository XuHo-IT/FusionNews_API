using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FusionNews_API.Migrations
{
    /// <inheritdoc />
    public partial class fixQuestionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tag_id",
                table: "chatbot_question",
                newName: "question_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "question_id",
                table: "chatbot_question",
                newName: "tag_id");
        }
    }
}
