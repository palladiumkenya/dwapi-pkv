﻿// <auto-generated />
using System;
using DwapiCentral.Cbs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DwapiCentral.Cbs.Infrastructure.Migrations
{
    [DbContext(typeof(CbsContext))]
    [Migration("20210206125345_ManifetsTag")]
    partial class ManifetsTag
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DwapiCentral.Cbs.Core.Model.Cargo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Items");

                    b.Property<Guid>("ManifestId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ManifestId");

                    b.ToTable("Cargoes");
                });

            modelBuilder.Entity("DwapiCentral.Cbs.Core.Model.Docket", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Instance");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Dockets");
                });

            modelBuilder.Entity("DwapiCentral.Cbs.Core.Model.Facility", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Emr");

                    b.Property<int?>("MasterFacilityId");

                    b.Property<string>("Name")
                        .HasMaxLength(120);

                    b.Property<int>("SiteCode");

                    b.Property<DateTime?>("SnapshotDate");

                    b.Property<int?>("SnapshotSiteCode");

                    b.Property<int?>("SnapshotVersion");

                    b.HasKey("Id");

                    b.HasIndex("MasterFacilityId");

                    b.ToTable("Facilities");
                });

            modelBuilder.Entity("DwapiCentral.Cbs.Core.Model.Manifest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateArrived");

                    b.Property<DateTime>("DateLogged");

                    b.Property<Guid?>("EmrId");

                    b.Property<string>("EmrName");

                    b.Property<int>("EmrSetup");

                    b.Property<DateTime?>("End");

                    b.Property<Guid>("FacilityId");

                    b.Property<int>("ManifestType");

                    b.Property<string>("Name");

                    b.Property<int>("Recieved");

                    b.Property<int>("Sent");

                    b.Property<Guid?>("Session");

                    b.Property<int>("SiteCode");

                    b.Property<DateTime?>("Start");

                    b.Property<int>("Status");

                    b.Property<DateTime>("StatusDate");

                    b.Property<string>("Tag");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.ToTable("Manifests");
                });

            modelBuilder.Entity("DwapiCentral.Cbs.Core.Model.MasterFacility", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("County")
                        .HasMaxLength(120);

                    b.Property<string>("Name")
                        .HasMaxLength(120);

                    b.Property<DateTime?>("SnapshotDate");

                    b.Property<int?>("SnapshotSiteCode");

                    b.Property<int?>("SnapshotVersion");

                    b.HasKey("Id");

                    b.ToTable("MasterFacilities");
                });

            modelBuilder.Entity("DwapiCentral.Cbs.Core.Model.MasterPatientIndex", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Birth_Certificate");

                    b.Property<string>("CCC_Number");

                    b.Property<string>("ContactAddress");

                    b.Property<string>("ContactName");

                    b.Property<string>("ContactPhoneNumber");

                    b.Property<string>("ContactRelation");

                    b.Property<DateTime?>("DOB");

                    b.Property<DateTime?>("DateConfirmedHIVPositive");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateExtracted");

                    b.Property<Guid>("FacilityId");

                    b.Property<string>("FacilityName");

                    b.Property<string>("FirstName");

                    b.Property<string>("FirstName_Normalized");

                    b.Property<string>("Gender");

                    b.Property<double?>("JaroWinklerScore");

                    b.Property<string>("LastName");

                    b.Property<string>("LastName_Normalized");

                    b.Property<string>("MaritalStatus");

                    b.Property<string>("MiddleName");

                    b.Property<string>("MiddleName_Normalized");

                    b.Property<string>("NHIF_Number");

                    b.Property<string>("National_ID");

                    b.Property<string>("PatientAlternatePhoneNumber");

                    b.Property<string>("PatientCounty");

                    b.Property<string>("PatientID");

                    b.Property<string>("PatientPhoneNumber");

                    b.Property<int>("PatientPk");

                    b.Property<string>("PatientSource");

                    b.Property<string>("PatientSubCounty");

                    b.Property<string>("PatientVillage");

                    b.Property<bool?>("Processed");

                    b.Property<string>("QueueId");

                    b.Property<Guid?>("RefId");

                    b.Property<int>("RowId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Serial");

                    b.Property<int>("SiteCode");

                    b.Property<DateTime?>("StartARTDate");

                    b.Property<string>("StartARTRegimenCode");

                    b.Property<string>("StartARTRegimenDesc");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("StatusDate");

                    b.Property<string>("TB_Number");

                    b.Property<string>("dmFirstName");

                    b.Property<string>("dmLastName");

                    b.Property<string>("dmMiddleName");

                    b.Property<string>("dmPKValue");

                    b.Property<string>("dmPKValueDoB");

                    b.Property<string>("sxFirstName");

                    b.Property<string>("sxLastName");

                    b.Property<string>("sxMiddleName");

                    b.Property<string>("sxPKValue");

                    b.Property<string>("sxPKValueDoB");

                    b.Property<string>("sxdmPKValue");

                    b.Property<string>("sxdmPKValueDoB");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.ToTable("MasterPatientIndices");
                });

            modelBuilder.Entity("DwapiCentral.Cbs.Core.Model.MetricMigrationExtract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("Dataset");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateExtracted");

                    b.Property<string>("Emr");

                    b.Property<Guid>("FacilityId");

                    b.Property<string>("Metric");

                    b.Property<int>("MetricId");

                    b.Property<string>("MetricValue");

                    b.Property<bool?>("Processed");

                    b.Property<string>("Project");

                    b.Property<string>("QueueId");

                    b.Property<int>("SiteCode");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("StatusDate");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.ToTable("MetricMigrationExtracts");
                });

            modelBuilder.Entity("DwapiCentral.Cbs.Core.Model.Subscriber", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthCode");

                    b.Property<string>("DocketId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DocketId");

                    b.ToTable("Subscribers");
                });

            modelBuilder.Entity("DwapiCentral.Cbs.Core.Model.Cargo", b =>
                {
                    b.HasOne("DwapiCentral.Cbs.Core.Model.Manifest")
                        .WithMany("Cargoes")
                        .HasForeignKey("ManifestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DwapiCentral.Cbs.Core.Model.Facility", b =>
                {
                    b.HasOne("DwapiCentral.Cbs.Core.Model.MasterFacility")
                        .WithMany("Mentions")
                        .HasForeignKey("MasterFacilityId");
                });

            modelBuilder.Entity("DwapiCentral.Cbs.Core.Model.Manifest", b =>
                {
                    b.HasOne("DwapiCentral.Cbs.Core.Model.Facility")
                        .WithMany("Manifests")
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DwapiCentral.Cbs.Core.Model.MasterPatientIndex", b =>
                {
                    b.HasOne("DwapiCentral.Cbs.Core.Model.Facility")
                        .WithMany("MasterPatientIndices")
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DwapiCentral.Cbs.Core.Model.MetricMigrationExtract", b =>
                {
                    b.HasOne("DwapiCentral.Cbs.Core.Model.Facility")
                        .WithMany("MetricMigrationExtracts")
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DwapiCentral.Cbs.Core.Model.Subscriber", b =>
                {
                    b.HasOne("DwapiCentral.Cbs.Core.Model.Docket")
                        .WithMany("Subscribers")
                        .HasForeignKey("DocketId");
                });
#pragma warning restore 612, 618
        }
    }
}