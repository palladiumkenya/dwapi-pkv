using System;
using System.Linq;
using DwapiCentral.Cbs.Core.Command;
using DwapiCentral.Cbs.Core.CommandHandler;
using DwapiCentral.Cbs.Core.Interfaces;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.Cbs.Infrastructure.Data;
using DwapiCentral.Cbs.Infrastructure.Data.Repository;
using DwapiCentral.SharedKernel.Exceptions;
using DwapiCentral.SharedKernel.Tests.TestData;
using DwapiCentral.SharedKernel.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace DwapiCentral.Cbs.Core.Tests.CommandHandler
{
    [TestFixture]
    public class CreateManifestHandlerTests
    {
        private ServiceProvider _serviceProvider;
        private IMediator _mediator;
        private CbsContext _context;
        private Manifest _manifest;

        [OneTimeSetUp]
        public void Init()
        {
            _serviceProvider = new ServiceCollection()
                .AddDbContext<CbsContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()))
                .AddScoped<IMasterFacilityRepository, MasterFacilityRepository>()
                .AddScoped<IFacilityRepository, FacilityRepository>()
                .AddScoped<IManifestRepository, ManifestRepository>()
                .AddMediatR(typeof(ValidateFacilityHandler))
                .BuildServiceProvider();


            _context = _serviceProvider.GetService<CbsContext>();
            _context.MasterFacilities.Add(new MasterFacility(1, "XFacility", "XCounty"));
            _context.MasterFacilities.Add(new MasterFacility(2, "YFacility", "YCounty"));
            _context.Facilities.Add(new Facility(1, "XFacility District", 1));
            _context.SaveChanges();

            _manifest = TestDataFactory.TestManifests(1, 2).First();
        }

        [SetUp]
        public void SetUp()
        {
            _mediator = _serviceProvider.GetService<IMediator>();
        }

        [Test]
        public void should_Throw_Exception_Invalid_SiteCode_Manifest()
        {
            var ex = Assert.Throws<System.AggregateException>(() => CreateManifest(3,"XFac",_manifest));
            Assert.AreEqual(typeof(FacilityNotFoundException), ex.InnerException.GetType());
            Console.WriteLine($"{ex.InnerException.Message}");
        }

        [Test]
        public void should_Create_Manifest_Enrolled_Facility()
        {
            var manifestId = CreateManifest(1, "X Hos", _manifest);

            var manifest = _context.Manifests.Find(manifestId);
            Assert.NotNull(manifest);            
            Assert.True(manifest.Cargoes.Count>0);

            var facility = _context.Facilities.Find(manifest.FacilityId);
            var mflfacility = _context.MasterFacilities.Find(facility.SiteCode);
            Assert.NotNull(facility);
            Assert.NotNull(mflfacility);
            Console.WriteLine(facility);
        }

        [Test]
        public void should_Create_Manifest_New_Facility()
        {
            var manifestId = CreateManifest(2, "Y Hos", _manifest);

            var manifest = _context.Manifests.Find(manifestId);
            Assert.NotNull(manifest);
            Assert.True(manifest.Cargoes.Count > 0);

            var facility = _context.Facilities.Find(manifest.FacilityId);
            var mflfacility = _context.MasterFacilities.Find(facility.SiteCode);
            Assert.NotNull(facility);
            Assert.NotNull(mflfacility);
            Console.WriteLine(facility);
        }
        private Guid CreateManifest(int siteCode,string name,Manifest manifest)
        {
            return _mediator.Send(new SaveManifest(siteCode,name,manifest)).Result;
        }
    }
}