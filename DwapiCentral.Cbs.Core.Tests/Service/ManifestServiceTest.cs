﻿using System;
using System.Collections.Generic;
using System.Linq;
using DwapiCentral.Cbs.Core.Command;
using DwapiCentral.Cbs.Core.CommandHandler;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Interfaces.Service;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.Cbs.Core.Service;
using DwapiCentral.Cbs.Infrastructure.Data;
using DwapiCentral.Cbs.Infrastructure.Data.Repository;
using DwapiCentral.SharedKernel.Custom;
using DwapiCentral.SharedKernel.Tests.TestData;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DwapiCentral.Cbs.Core.Tests.Service
{
    public class ManifestServiceTest
    {
        private ServiceProvider _serviceProvider;
        private CbsContext _context;
        private IManifestService _manifestService;
        private IMediator _mediator;

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
                .AddScoped<IMasterFacilityRepository, MasterFacilityRepository>()
                .AddScoped<IMasterPatientIndexRepository, MasterPatientIndexRepository>()
                .AddScoped<IManifestRepository, ManifestRepository>()
                .AddScoped<IManifestService, ManifestService>()
                .AddMediatR(typeof(ValidateFacilityHandler))
                .BuildServiceProvider();


            _context = _serviceProvider.GetService<CbsContext>();
            _context.Database.EnsureDeleted();
            _context.Database.Migrate();
            _context.MasterFacilities.AddRange(TestDataFactory.TestMasterFacilities());
            var facilities = TestDataFactory.TestFacilities();
            _context.Facilities.AddRange(facilities);
            _context.SaveChanges();
            _context.MasterPatientIndices.AddRange(TestDataFactory.TestMasterPatientIndices(1, facilities.First(x => x.SiteCode == 1).Id));
            _context.MasterPatientIndices.AddRange(TestDataFactory.TestMasterPatientIndices(2, facilities.First(x => x.SiteCode == 2).Id));
            _context.SaveChanges();

            //1, 
        }

        [SetUp]
        public void SetUp()
        {
            _manifestService = _serviceProvider.GetService<IManifestService>();
            _mediator = _serviceProvider.GetService<IMediator>();
        }

        [Test]
        public void should_Clear_By_Site()
        {
            var sitePatients = _context.MasterPatientIndices.ToList();
            Assert.True(sitePatients.Any(x=>x.SiteCode==1));
            Assert.True(sitePatients.Any(x => x.SiteCode == 2));

            var manifests = TestDataFactory.TestManifests(1);
            manifests.ForEach(x => x.SiteCode = 1);
            var id=_mediator.Send(new SaveManifest(manifests.First())).Result;
            _manifestService.Process();

            var remainingPatients = _context.MasterPatientIndices.ToList();
            Assert.False(remainingPatients.Any(x => x.SiteCode == 1));
            Assert.True(remainingPatients.Any(x => x.SiteCode == 2));
        }
    }
}
