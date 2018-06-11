using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DwapiCentral.Cbs.Infrastructure.Migrations
{
    public partial class CbsInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dockets",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Instance = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dockets", x => x.Id);
                });

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
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AuthCode = table.Column<string>(nullable: true),
                    DocketId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscribers_Dockets_DocketId",
                        column: x => x.DocketId,
                        principalTable: "Dockets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Manifests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SiteCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Sent = table.Column<int>(nullable: false),
                    Recieved = table.Column<int>(nullable: false),
                    DateLogged = table.Column<DateTime>(nullable: false),
                    DateArrived = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StatusDate = table.Column<DateTime>(nullable: false),
                    FacilityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manifests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manifests_Facilities_FacilityId",
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
                    RowId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatientPk = table.Column<int>(nullable: false),
                    SiteCode = table.Column<int>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    Serial = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FirstName_Normalized = table.Column<string>(nullable: true),
                    MiddleName_Normalized = table.Column<string>(nullable: true),
                    LastName_Normalized = table.Column<string>(nullable: true),
                    PatientPhoneNumber = table.Column<string>(nullable: true),
                    PatientAlternatePhoneNumber = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    PatientSource = table.Column<string>(nullable: true),
                    PatientCounty = table.Column<string>(nullable: true),
                    PatientSubCounty = table.Column<string>(nullable: true),
                    PatientVillage = table.Column<string>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    National_ID = table.Column<string>(nullable: true),
                    NHIF_Number = table.Column<string>(nullable: true),
                    Birth_Certificate = table.Column<string>(nullable: true),
                    CCC_Number = table.Column<string>(nullable: true),
                    TB_Number = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    ContactRelation = table.Column<string>(nullable: true),
                    ContactPhoneNumber = table.Column<string>(nullable: true),
                    ContactAddress = table.Column<string>(nullable: true),
                    DateConfirmedHIVPositive = table.Column<DateTime>(nullable: true),
                    StartARTDate = table.Column<DateTime>(nullable: true),
                    StartARTRegimenCode = table.Column<string>(nullable: true),
                    StartARTRegimenDesc = table.Column<string>(nullable: true),
                    dmFirstName = table.Column<string>(nullable: true),
                    dmLastName = table.Column<string>(nullable: true),
                    sxFirstName = table.Column<string>(nullable: true),
                    sxLastName = table.Column<string>(nullable: true),
                    sxPKValue = table.Column<string>(nullable: true),
                    dmPKValue = table.Column<string>(nullable: true),
                    sxdmPKValue = table.Column<string>(nullable: true),
                    sxMiddleName = table.Column<string>(nullable: true),
                    dmMiddleName = table.Column<string>(nullable: true),
                    sxPKValueDoB = table.Column<string>(nullable: true),
                    dmPKValueDoB = table.Column<string>(nullable: true),
                    sxdmPKValueDoB = table.Column<string>(nullable: true),
                    JaroWinklerScore = table.Column<double>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    Processed = table.Column<bool>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Cargoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Items = table.Column<string>(nullable: true),
                    ManifestId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cargoes_Manifests_ManifestId",
                        column: x => x.ManifestId,
                        principalTable: "Manifests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes_ManifestId",
                table: "Cargoes",
                column: "ManifestId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_MasterFacilityId",
                table: "Facilities",
                column: "MasterFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Manifests_FacilityId",
                table: "Manifests",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPatientIndices_FacilityId",
                table: "MasterPatientIndices",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_DocketId",
                table: "Subscribers",
                column: "DocketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cargoes");

            migrationBuilder.DropTable(
                name: "MasterPatientIndices");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "Manifests");

            migrationBuilder.DropTable(
                name: "Dockets");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "MasterFacilities");
        }
    }
}
