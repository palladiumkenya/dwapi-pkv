using System;
using DwapiCentral.SharedKernel.Infrastructure.Data;
using DwapiCentral.SharedKernel.Infrastructure.Tests.Data;
using DwapiCentral.SharedKernel.Tests.TestData.Interfaces;
using DwapiCentral.SharedKernel.Tests.TestData.Models;
using Microsoft.EntityFrameworkCore;

namespace DwapiCentral.SharedKernel.Infrastructure.Tests.TestData
{
    
    public class TestCarRepository :BaseRepository<TestCar,Guid>,  ITestCarRepository
    {
        public TestCarRepository(DbContext context) : base(context)
        {
        }
    }
}