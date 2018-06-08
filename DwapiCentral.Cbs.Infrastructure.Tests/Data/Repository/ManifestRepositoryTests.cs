using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.Cbs.Infrastructure.Data;
using DwapiCentral.Cbs.Infrastructure.Data.Repository;
using DwapiCentral.SharedKernel.Exceptions;
using DwapiCentral.SharedKernel.Tests.TestData;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DwapiCentral.Cbs.Infrastructure.Tests.Data.Repository
{
    [TestFixture]
    [Category("UsesDb")]
    public class ManifestRepositoryTests
    {
        private ServiceProvider _serviceProvider;
        private CbsContext _context;
        private List<Facility> _facilities;
        private IManifestRepository _manifestRepository;
        private List<Manifest> _manifests;

        [OneTimeSetUp]
        public void Init()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["ConnectionStrings:DwapiConnectionDev"];

            _serviceProvider = new ServiceCollection()
                .AddDbContext<CbsContext>(o => o.UseSqlServer(connectionString))
                .AddTransient<IManifestRepository, ManifestRepository>()
                .BuildServiceProvider();

            _facilities = TestDataFactory.TestFacilityWithPatients(2);
            _manifests = TestDataFactory.TestManifests(2);

            _manifests[0].FacilityId = _facilities[0].Id;
            _manifests[1].FacilityId = _facilities[1].Id;

            _context = _serviceProvider.GetService<CbsContext>();
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _context.MasterFacilities.AddRange(TestDataFactory.TestMasterFacilities());
            _context.Facilities.AddRange(_facilities);
            _context.Manifests.AddRange(_manifests);
            _context.SaveChanges();
        }

        [SetUp]
        public void Setup()
        {
            _manifestRepository = _serviceProvider.GetService<IManifestRepository>();
        }

        [Test]
        public void should_Clear_With_Manifest_Facility()
        {
            var patients = _context.MasterPatientIndices;
            Assert.True(patients.Any());
            var count= _manifestRepository.ClearFacility(_manifests).Result;
            var nopatients = _context.MasterPatientIndices;
            Assert.False(nopatients.Any());
        }
    }
}