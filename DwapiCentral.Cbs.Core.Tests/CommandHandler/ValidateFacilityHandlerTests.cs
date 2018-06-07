using System;
using DwapiCentral.Cbs.Core.Command;
using DwapiCentral.Cbs.Core.CommandHandler;
using DwapiCentral.Cbs.Core.Interfaces;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.Cbs.Infrastructure.Data;
using DwapiCentral.Cbs.Infrastructure.Data.Repository;
using DwapiCentral.SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DwapiCentral.Cbs.Core.Tests.CommandHandler
{
    [TestFixture]
    public class ValidateFacilityHandlerTests
    {
        private ValidateFacilityHandler _handler;
        private ServiceProvider _serviceProvider;
        private IMediator _mediator;
        private CbsContext _context;

        [OneTimeSetUp]
        public void Init()
        {
            _serviceProvider = new ServiceCollection()
                .AddDbContext<CbsContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()))
                .AddScoped<IMasterFacilityRepository, MasterFacilityRepository>()
                .AddMediatR(typeof(ValidateFacilityHandler))
                .BuildServiceProvider();

            
            _context = _serviceProvider.GetService<CbsContext>();
            _context.MasterFacilities.Add(new MasterFacility(1, "XFacility", "XCounty"));
            _context.SaveChanges();
        }

        [SetUp]
        public void SetUp()
        {
            _mediator = _serviceProvider.GetService<IMediator>();
        }

        [Test]
        public void should_Throw_Exception_Invalid_SiteCode()
        {
          var ex=  Assert.Throws<System.AggregateException>(() =>CheckFacility(2));
            Assert.AreEqual(typeof(FacilityNotFoundException),ex.InnerException.GetType());
            Console.WriteLine($"{ex.InnerException.Message}");
        }

        [Test]
        public void should_return_Validated_Facility()
        {
            var fac = CheckFacility(1);
            Assert.False(string.IsNullOrWhiteSpace(fac));
            Console.WriteLine(fac);
        }

        private  string CheckFacility(int code)
        {
            return _mediator.Send(new ValidateFacility(code)).Result;
        }
    }
}