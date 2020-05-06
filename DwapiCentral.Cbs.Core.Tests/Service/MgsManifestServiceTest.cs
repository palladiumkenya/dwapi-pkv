using System.Linq;
using DwapiCentral.Cbs.Core.Command;
using DwapiCentral.Cbs.Core.Interfaces.Service;
using DwapiCentral.Cbs.Infrastructure.Data;
using DwapiCentral.SharedKernel.Tests.TestData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DwapiCentral.Cbs.Core.Tests.Service
{
    public class MgsManifestServiceTest
    {
        private CbsContext _context;
        private IMgsManifestService _manifestService;
        private IMediator _mediator;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
            TestInitializer.SeedData(TestDataFactory.TestMasterFacilities());
            var facilities = TestDataFactory.TestFacilities();
            TestInitializer.SeedData(facilities);
            TestInitializer.SeedData(
                TestDataFactory.TestMetricMigrationExtracts(facilities.First().SiteCode, facilities.First().Id));
            TestInitializer.SeedData(
                TestDataFactory.TestMetricMigrationExtracts(facilities.Last().SiteCode, facilities.Last().Id));
        }

        [SetUp]
        public void SetUp()
        {
            _context = TestInitializer.ServiceProvider.GetService<CbsContext>();
            _manifestService = TestInitializer.ServiceProvider.GetService<IMgsManifestService>();
            _mediator = TestInitializer.ServiceProvider.GetService<IMediator>();
        }

        [Test]
        public void should_Process()
        {
            var sitePatients = _context.MetricMigrationExtracts.ToList();
            Assert.True(sitePatients.Any(x => x.SiteCode == 14950));
            Assert.True(sitePatients.Any(x => x.SiteCode == 12618));

            var manifest = TestDataFactory.TestMgsManifests().First(x => x.SiteCode == 14950);
            var id = _mediator.Send(new SaveManifest(manifest)).Result;
            _manifestService.Process(false);

            var remainingPatients = _context.MetricMigrationExtracts.ToList();
            Assert.False(remainingPatients.Any(x => x.SiteCode == 14950));
            Assert.True(remainingPatients.Any(x => x.SiteCode == 12618));
        }
    }
}
