using System.Collections.Generic;
using System.Linq;
using DwapiCentral.Cbs.Core.Command;
using DwapiCentral.Cbs.Core.Interfaces.Service;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.Cbs.Infrastructure.Data;
using DwapiCentral.SharedKernel.Tests.TestData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DwapiCentral.Cbs.Core.Tests.Service
{
    public class MpiServiceTests
    {
        private List<MasterPatientIndex> _patientIndices;
        private List<MasterPatientIndex> _patientIndicesSiteB;
        private CbsContext _context;
        private IMpiService _mpiService;
        private IManifestService _manifestService;
        private IMediator _mediator;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
            TestInitializer.SeedData(TestDataFactory.TestMasterFacilities());
            var facilities = TestDataFactory.TestFacilities();
            TestInitializer.SeedData(facilities);
            _patientIndices = TestDataFactory.TestMasterPatientIndices(14950, facilities.First().Id);
            _patientIndicesSiteB = TestDataFactory.TestMasterPatientIndices(12618, facilities.Last().Id);
        }

        [SetUp]
        public void SetUp()
        {
            _manifestService =TestInitializer.ServiceProvider.GetService<IManifestService>();
            _mediator = TestInitializer.ServiceProvider.GetService<IMediator>();
            _mpiService = TestInitializer.ServiceProvider.GetService<IMpiService>();
            _context= TestInitializer.ServiceProvider.GetService<CbsContext>();

        }
        [Test]
        public void should_Process()
        {
            Assert.False(_context.MasterPatientIndices.Any(x => x.SiteCode==14950));

            _mpiService.Process(_patientIndices,false);

            Assert.True(_context.MasterPatientIndices.Any(x => x.SiteCode==14950));
        }

        [Test]
        public void should_Process_After_Manifest()
        {
            var manifest = TestDataFactory.TestManifests().First(x => x.SiteCode == 12618);
            var mid = _mediator.Send(new SaveManifest(manifest)).Result;
            _manifestService.Process();
            Assert.False(_context.MasterPatientIndices.Any(x => x.SiteCode==12618));

            _mpiService.Process(_patientIndicesSiteB);

            Assert.True(_context.MasterPatientIndices.Any(x=>x.SiteCode==12618));
        }
    }
}
