using System;
using System.Linq;
using DwapiCentral.Cbs.Core.Command;
using DwapiCentral.Cbs.Core.CommandHandler;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.Cbs.Infrastructure.Data;
using DwapiCentral.Cbs.Infrastructure.Data.Repository;
using DwapiCentral.SharedKernel.Exceptions;
using DwapiCentral.SharedKernel.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DwapiCentral.Cbs.Core.Tests.CommandHandler
{
    [TestFixture]
    public class SnapMasterFacilityHandlerTests

    {
        private ServiceProvider _serviceProvider;
        private IMediator _mediator;
        private CbsContext _context;

        [OneTimeSetUp]
        public void Init()
        {
            _serviceProvider = new ServiceCollection()
                .AddDbContext<CbsContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()))
                .AddScoped<IMasterFacilityRepository, MasterFacilityRepository>()
                .AddScoped<IFacilityRepository, FacilityRepository>()
                .AddMediatR(typeof(ValidateFacilityHandler))
                .BuildServiceProvider();


            _context = _serviceProvider.GetService<CbsContext>();
            _context.MasterFacilities.Add(new MasterFacility(1, "XFacility", "XCounty"));
            _context.MasterFacilities.Add(new MasterFacility(2, "YFacility", "YCounty"));
            _context.Facilities.Add(new Facility(1, "XFacility District", 1){Emr = "IQCare"});
            _context.SaveChanges();
        }

        [SetUp]
        public void SetUp()
        {
            _mediator = _serviceProvider.GetService<IMediator>();
        }

        [Test]
        public void should_Snap_Master_Facility()
        {
            var firstCommand = _mediator.Send(new SnapMasterFacility(1)).Result;
            Assert.True(firstCommand);

            var secondCommand = _mediator.Send(new SnapMasterFacility(1)).Result;
            Assert.True(secondCommand);

            var originalMasterFacility = _context.MasterFacilities.Find(1);
            var masterFacility = _context.MasterFacilities.Find(-1011);
            var masterFacilityVer2 = _context.MasterFacilities.Find(-1021);
            var facility = _context.Facilities.First(x=>x.SiteCode==-1011);

            Assert.NotNull(masterFacility);
            Assert.NotNull(masterFacilityVer2);
            Assert.NotNull(facility);

            Console.WriteLine($"{originalMasterFacility.Id},{originalMasterFacility}");
            Console.WriteLine($"{masterFacility.Id},{masterFacility}");
            Console.WriteLine($"{masterFacilityVer2.Id},{masterFacility}");
            Console.WriteLine($"{facility}");
        }

    }
}
