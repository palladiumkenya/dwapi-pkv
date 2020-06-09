using System;
using System.Collections.Generic;
using DwapiCentral.Cbs.Core.Exchange;
using DwapiCentral.Cbs.Core.Interfaces.Service;
using DwapiCentral.Cbs.Core.Model.Dto;
using NUnit.Framework;

namespace DwapiCentral.Cbs.Core.Tests.Service
{
    [TestFixture]
    public class LiveSyncServiceTests
    {
        private ILiveSyncService _liveSyncService;
        private List<ManifestDto> _manifestDtos = new List<ManifestDto>();
        private List<MetricDto> _metricDtos = new List<MetricDto>();
        private readonly Guid fmId = Guid.NewGuid();


        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void should_Sync_Manifest()
        {

        }

        [Test]
        public void should_Sync_Stats()
        {

        }

        [Test]
        public void should_Sync_Metrics()
        {

        }
    }
}
