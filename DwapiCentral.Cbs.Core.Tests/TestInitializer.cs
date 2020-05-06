using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Dapper;
using DwapiCentral.Cbs.Core.CommandHandler;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Interfaces.Service;
using DwapiCentral.Cbs.Core.Service;
using DwapiCentral.Cbs.Infrastructure.Data;
using DwapiCentral.Cbs.Infrastructure.Data.Repository;
using DwapiCentral.SharedKernel.Utils;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;
using Z.Dapper.Plus;

namespace DwapiCentral.Cbs.Core.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        public static IServiceProvider ServiceProvider;
        public static IServiceCollection Services;
        public static string ConnectionString;
        public static string MpiConnectionString;
        public static IConfigurationRoot Configuration;
        private readonly string _liveSync = "http://localhost:4777/stages/";

        [OneTimeSetUp]
        public void Init()
        {
            SqlMapper.AddTypeHandler(new GuidTypeHandler());

            RegisterLicence();
            RemoveTestsFilesDbs();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            var dir = $"{TestContext.CurrentContext.TestDirectory.HasToEndWith(@"/")}";

            var config = Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = config.GetConnectionString("DwapiConnection");
            ConnectionString = connectionString;


            var services = new ServiceCollection()
                .AddDbContext<CbsContext>(x => x.UseSqlServer(connectionString));
            services.AddScoped<IDocketRepository, DocketRepository>();
            services.AddScoped<IMasterFacilityRepository, MasterFacilityRepository>();
            services.AddScoped<IFacilityRepository, FacilityRepository>();
            services.AddScoped<IManifestRepository, ManifestRepository>();
            services.AddScoped<IMasterPatientIndexRepository, MasterPatientIndexRepository>();
            services.AddScoped<IMetricMigrationExtractRepository, MetricMigrationExtractRepository>();
            services.AddScoped<IManifestService, ManifestService>();
            services.AddScoped<IMgsManifestService, MgsManifestService>();
            services.AddScoped<IMpiService, MpiService>();
            services.AddScoped<IMgsService, MgsService>();
            services.AddMediatR(typeof(ValidateFacilityHandler));
            services.AddScoped<ILiveSyncService, LiveSyncService>();

            if (!string.IsNullOrWhiteSpace(_liveSync))
            {
                Uri endPointA = new Uri(_liveSync); // this is the endpoint HttpClient will hit
                HttpClient httpClient = new HttpClient()
                {
                    BaseAddress = endPointA,
                };
                services.AddSingleton<HttpClient>(httpClient);
            }
            Services = services;

            ServiceProvider = Services.BuildServiceProvider();
      }

        public static void ClearDb()
        {
            var context = ServiceProvider.GetService<CbsContext>();
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            context.EnsureSeeded();
        }
        public static void SeedData(params IEnumerable<object>[] entities)
        {
            var context = ServiceProvider.GetService<CbsContext>();
            foreach (IEnumerable<object> t in entities)
            {
                context.AddRange(t);
            }
            context.SaveChanges();
        }

        private void RegisterLicence()
        {
            DapperPlusManager.AddLicense("1755;700-ThePalladiumGroup", "2073303b-0cfc-fbb9-d45f-1723bb282a3c");
            if (!Z.Dapper.Plus.DapperPlusManager.ValidateLicense(out var licenseErrorMessage))
            {
                throw new Exception(licenseErrorMessage);
            }
        }

        private void RemoveTestsFilesDbs()
        {
            string[] keyFiles =
                { "cbs.db"};
            string[] keyDirs = { @"TestArtifacts/Database".ToOsStyle()};

            foreach (var keyDir in keyDirs)
            {
                DirectoryInfo di = new DirectoryInfo(keyDir);
                foreach (FileInfo file in di.GetFiles())
                {
                    if (!keyFiles.Contains(file.Name))
                        file.Delete();
                }
            }
        }
    }
}
