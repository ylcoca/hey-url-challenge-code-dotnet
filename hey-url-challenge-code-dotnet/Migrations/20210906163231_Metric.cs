using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hey_url_challenge_code_dotnet.Migrations
{
    public partial class Metric : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Metric",
                columns: table => new
                {
                    MetricId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Broswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OS = table.Column<int>(type: "int", nullable: false),
                    UrlId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metric", x => x.MetricId);
                    table.ForeignKey(
                        name: "FK_Metric_Urls_UrlId",
                        column: x => x.UrlId,
                        principalTable: "Urls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Metric_UrlId",
                table: "Metric",
                column: "UrlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Metric");
        }
    }
}
