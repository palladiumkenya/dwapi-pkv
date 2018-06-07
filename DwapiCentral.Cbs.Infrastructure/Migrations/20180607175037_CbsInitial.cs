using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DwapiCentral.Cbs.Infrastructure.Migrations
{
    public partial class CbsInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterFacilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 120, nullable: true),
                    County = table.Column<string>(maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterFacilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SiteCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 120, nullable: true),
                    MasterFacilityId = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facilities_MasterFacilities_MasterFacilityId",
                        column: x => x.MasterFacilityId,
                        principalTable: "MasterFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacilityManifests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AmountSent = table.Column<int>(nullable: false),
                    AmountRecievied = table.Column<int>(nullable: false),
                    FacilityId = table.Column<Guid>(nullable: false),
                    DateLogged = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityManifests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacilityManifests_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasterPatientIndices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientPk = table.Column<int>(nullable: false),
                    SiteCode = table.Column<int>(nullable: false),
                    FacilityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterPatientIndices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterPatientIndices_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_MasterFacilityId",
                table: "Facilities",
                column: "MasterFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityManifests_FacilityId",
                table: "FacilityManifests",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPatientIndices_FacilityId",
                table: "MasterPatientIndices",
                column: "FacilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacilityManifests");

            migrationBuilder.DropTable(
                name: "MasterPatientIndices");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "MasterFacilities");
        }
    }
}
