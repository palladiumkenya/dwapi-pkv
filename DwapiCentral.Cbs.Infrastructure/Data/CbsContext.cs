using System.ComponentModel.DataAnnotations;
using System.Reflection;
using CsvHelper.Configuration;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Infrastructure.Data;
using EFCore.Seeder.Configuration;
using EFCore.Seeder.Extensions;
using Microsoft.EntityFrameworkCore;
using Z.Dapper.Plus;

namespace DwapiCentral.Cbs.Infrastructure.Data
{
    public class CbsContext:BaseContext
    {
        public DbSet<MasterFacility> MasterFacilities { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityManifest> FacilityManifests { get; set; }
        public DbSet<MasterPatientIndex> MasterPatientIndices { get; set; }

        public CbsContext(DbContextOptions<CbsContext> options) : base(options)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            DapperPlusManager.Entity<MasterFacility>().Key(x => x.Id).Table($"{nameof(CbsContext.MasterFacilities)}");
            DapperPlusManager.Entity<Facility>().Key(x => x.Id).Table($"{nameof(CbsContext.Facilities)}");
            DapperPlusManager.Entity<FacilityManifest>().Key(x => x.Id).Table($"{nameof(CbsContext.FacilityManifests)}");
            DapperPlusManager.Entity<MasterPatientIndex>().Key(x => x.Id).Table($"{nameof(CbsContext.MasterPatientIndices)}");
        }

        public override void EnsureSeeded()
        {
            var csvConfig = new CsvConfiguration
            {
                Delimiter = "|",
                SkipEmptyRecords = true,
                TrimFields = true,
                TrimHeaders = true,
                WillThrowOnMissingField = false
            };

            SeederConfiguration.ResetConfiguration(csvConfig, null, typeof(CbsContext).GetTypeInfo().Assembly);

            MasterFacilities.SeedDbSetIfEmpty($"{nameof(MasterFacility)}");
            SaveChanges();
        }
    }
}