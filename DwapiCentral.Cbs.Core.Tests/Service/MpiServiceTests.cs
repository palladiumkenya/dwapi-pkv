using System;
using System.Collections.Generic;
using System.Linq;
using DwapiCentral.Cbs.Core.CommandHandler;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Interfaces.Service;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.Cbs.Core.Service;
using DwapiCentral.Cbs.Infrastructure.Data;
using DwapiCentral.Cbs.Infrastructure.Data.Repository;
using DwapiCentral.SharedKernel.Tests.TestData;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DwapiCentral.Cbs.Core.Tests.Service
{
    public class MpiServiceTests
    {
        private ServiceProvider _serviceProvider;
        private List<MasterPatientIndex> _patientIndices;
        private CbsContext _context;
        private IMpiService _mpiService;

        [OneTimeSetUp]
        public void Init()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["ConnectionStrings:DwapiConnectionDev"];


            _serviceProvider = new ServiceCollection()
                .AddDbContext<CbsContext>(o => o.UseSqlServer(connectionString))
                .AddScoped<IFacilityRepository, FacilityRepository>()
                .AddScoped<IMasterPatientIndexRepository, MasterPatientIndexRepository>()
                .AddScoped<IMpiService, MpiService>()
                .AddMediatR(typeof(ValidateFacilityHandler))
                .BuildServiceProvider();


            _context = _serviceProvider.GetService<CbsContext>();
            _context.Database.EnsureDeleted();
            _context.Database.Migrate();
            _context.MasterFacilities.AddRange(TestDataFactory.TestMasterFacilities());
            _context.Facilities.AddRange(TestDataFactory.TestFacilities());
            _context.SaveChanges();

            _patientIndices = TestDataFactory.TestMasterPatientIndices(1);
        }

        [SetUp]
        public void SetUp()
        {
            _mpiService = _serviceProvider.GetService<IMpiService>();
        }
        [Test]
        public void should_Process()
        {
            var patients = _context.MasterPatientIndices.ToList();
            Assert.False(patients.Any());

            _mpiService.Process(_patientIndices);
            var savedPatients = _context.MasterPatientIndices.ToList();
            Assert.True(savedPatients.Any());
        }
    }
}
