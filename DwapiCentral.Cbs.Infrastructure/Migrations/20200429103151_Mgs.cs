using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DwapiCentral.Cbs.Infrastructure.Migrations
{
    public partial class Mgs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MetricMigrationExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MetricId = table.Column<int>(nullable: false),
                    SiteCode = table.Column<int>(nullable: false),
                    Dataset = table.Column<string>(nullable: true),
                    Metric = table.Column<string>(nullable: true),
                    MetricValue = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    Processed = table.Column<bool>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    FacilityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetricMigrationExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetricMigrationExtracts_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MetricMigrationExtracts_FacilityId",
                table: "MetricMigrationExtracts",
                column: "FacilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetricMigrationExtracts");
        }
    }
}
