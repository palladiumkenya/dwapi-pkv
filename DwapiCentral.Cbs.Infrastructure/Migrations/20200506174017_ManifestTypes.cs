using Microsoft.EntityFrameworkCore.Migrations;

namespace DwapiCentral.Cbs.Infrastructure.Migrations
{
    public partial class ManifestTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManifestType",
                table: "Manifests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManifestType",
                table: "Manifests");
        }
    }
}
