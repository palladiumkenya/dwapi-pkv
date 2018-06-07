using System;
using DwapiCentral.SharedKernel.Interfaces;
using DwapiCentral.SharedKernel.Tests.TestData.Models;

namespace DwapiCentral.SharedKernel.Tests.TestData.Interfaces
{
    public interface ITestCarRepository : IRepository<TestCar,Guid>
    {
        
    }
}