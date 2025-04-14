using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FusionNews_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_post_news_of_post_id",
                table: "post");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_on",
                table: "post",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.CreateIndex(
                name: "IX_post_news_of_post_id",
                table: "post",
                column: "news_of_post_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_post_news_of_post_id",
                table: "post");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_on",
                table: "post",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateIndex(
                name: "IX_post_news_of_post_id",
                table: "post",
                column: "news_of_post_id",
                unique: true);
        }
    }
}
