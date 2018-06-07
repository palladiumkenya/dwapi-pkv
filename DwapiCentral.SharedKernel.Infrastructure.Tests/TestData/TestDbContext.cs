using System.Configuration;
using System.Reflection;
using CsvHelper.Configuration;
using DwapiCentral.SharedKernel.Infrastructure.Data;
using DwapiCentral.SharedKernel.Tests.TestData.Models;
using EFCore.Seeder.Configuration;
using EFCore.Seeder.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DwapiCentral.SharedKernel.Infrastructure.Tests.TestData
{
    public class TestDbContext:BaseContext
    {
        public DbSet<TestCar> TestCars { get; set; }
        public DbSet<TestModel> TestModels { get; set; }

        public TestDbContext(DbContextOptions options) : base(options)
        {
        }

        public override void EnsureSeeded()
        {
            var csvConfig = new CsvConfiguration
            {
                SkipEmptyRecords = true,
                TrimFields = true,
                TrimHeaders = true,
                WillThrowOnMissingField = false
            };
            SeederConfiguration.ResetConfiguration(csvConfig, null, typeof(SettingsContext).GetTypeInfo().Assembly);

            TestCars.SeedDbSetIfEmpty($"{nameof(TestCar)}");
            TestModels.SeedDbSetIfEmpty($"{nameof(TestModel)}");

            base.EnsureSeeded();
        }
    }
}