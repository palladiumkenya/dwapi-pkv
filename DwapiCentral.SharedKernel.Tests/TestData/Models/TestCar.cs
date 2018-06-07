using System;
using System.Collections.Generic;
using DwapiCentral.SharedKernel.Model;

namespace DwapiCentral.SharedKernel.Tests.TestData.Models
{
    public class TestCar:Entity<Guid>
    {
        public string Name { get; set; }
        public ICollection<TestModel> Models { get; set; }=new List<TestModel>();
    }
}
