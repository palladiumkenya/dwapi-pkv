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
        public DbSet<Docket> Dockets { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<MasterFacility> MasterFacilities { get; set; }

        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Manifest> Manifests { get; set; }
        public DbSet<Cargo> Cargoes { get; set; }
        public DbSet<MasterPatientIndex> MasterPatientIndices { get; set; }
        public DbSet<MetricMigrationExtract> MetricMigrationExtracts { get; set; }

        public CbsContext(DbContextOptions<CbsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<MasterPatientIndex>()
                .Property(u => u.RowId)
                .UseSqlServerIdentityColumn();

            DapperPlusManager.Entity<Docket>().Key(x => x.Id).Table($"{nameof(CbsContext.Dockets)}");
            DapperPlusManager.Entity<Subscriber>().Key(x => x.Id).Table($"{nameof(CbsContext.Subscribers)}");

            DapperPlusManager.Entity<MasterFacility>().Key(x => x.Id).Table($"{nameof(CbsContext.MasterFacilities)}");

            DapperPlusManager.Entity<Facility>().Key(x => x.Id).Table($"{nameof(CbsContext.Facilities)}");
            DapperPlusManager.Entity<Manifest>().Key(x => x.Id).Table($"{nameof(CbsContext.Manifests)}");
            DapperPlusManager.Entity<Cargo>().Key(x => x.Id).Table($"{nameof(CbsContext.Cargoes)}");
            DapperPlusManager.Entity<MasterPatientIndex>().Key(x => x.Id).Table($"{nameof(CbsContext.MasterPatientIndices)}");
            DapperPlusManager.Entity<MetricMigrationExtract>().Key(x => x.Id).Table($"{nameof(CbsContext.MetricMigrationExtracts)}");

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

            //MasterFacilities.SeedDbSetIfEmpty($"{nameof(MasterFacility)}");
            Dockets.SeedDbSetIfEmpty($"{nameof(Docket)}");
            SaveChanges();
            Subscribers.SeedDbSetIfEmpty($"{nameof(Subscriber)}");
            SaveChanges();
        }
    }
}
