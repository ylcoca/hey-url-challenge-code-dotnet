using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hey_url_challenge_code_dotnet.Migrations
{
    public partial class MetricDateAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Metric_Urls_UrlId",
                table: "Metric");

            migrationBuilder.RenameColumn(
                name: "UrlId",
                table: "Metric",
                newName: "URLId");

            migrationBuilder.RenameIndex(
                name: "IX_Metric_UrlId",
                table: "Metric",
                newName: "IX_Metric_URLId");

            migrationBuilder.AlterColumn<Guid>(
                name: "URLId",
                table: "Metric",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Clicked",
                table: "Metric",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Metric_Urls_URLId",
                table: "Metric",
                column: "URLId",
                principalTable: "Urls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Metric_Urls_URLId",
                table: "Metric");

            migrationBuilder.DropColumn(
                name: "Clicked",
                table: "Metric");

            migrationBuilder.RenameColumn(
                name: "URLId",
                table: "Metric",
                newName: "UrlId");

            migrationBuilder.RenameIndex(
                name: "IX_Metric_URLId",
                table: "Metric",
                newName: "IX_Metric_UrlId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UrlId",
                table: "Metric",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Metric_Urls_UrlId",
                table: "Metric",
                column: "UrlId",
                principalTable: "Urls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
