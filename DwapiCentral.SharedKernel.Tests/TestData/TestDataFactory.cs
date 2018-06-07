using System.Collections.Generic;
using System.Linq;
using DwapiCentral.SharedKernel.Tests.TestData.Models;
using FizzWare.NBuilder;

namespace DwapiCentral.SharedKernel.Tests.TestData
{
    public class TestDataFactory
    {
        public static List<TestCar> TestCars(int count=2)
        {
            return Builder<TestCar>.CreateListOfSize(count)
                .Build()
                .ToList();
        }
        public static List<TestModel> TestModels(int count = 2)
        {
            return Builder<TestModel>.CreateListOfSize(count)
                .Build()
                .ToList();
        }
        public static List<TestCar> TestCarsWithModel(int count = 2,int childcount=3)
        {
            var cars= Builder<TestCar>.CreateListOfSize(count)
                .Build()
                .ToList();
            foreach (var testCar in cars)
            {
                testCar.Models = Builder<TestModel>.CreateListOfSize(childcount)
                    .All()
                    .With(x=>x.TestCarId==testCar.Id)
                    .Build()
                    .ToList();
            }
            return cars;
        }
    }
}