using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DwapiCentral.Cbs.Infrastructure.Migrations
{
    public partial class RefSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RefId",
                table: "MasterPatientIndices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefId",
                table: "MasterPatientIndices");
        }
    }
}
