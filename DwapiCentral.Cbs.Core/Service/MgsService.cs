using System;
using System.Collections.Generic;
using System.Linq;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Interfaces.Service;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Exceptions;
using DwapiCentral.SharedKernel.Model;
using Serilog;

namespace DwapiCentral.Cbs.Core.Service
{
    public class MgsService : IMgsService
    {
        private readonly IMetricMigrationExtractRepository _metricMigrationExtractRepository;
        private readonly IFacilityRepository _facilityRepository;
        private List<SiteProfile> _siteProfiles = new List<SiteProfile>();

        public MgsService(IMetricMigrationExtractRepository metricMigrationExtractRepository, IFacilityRepository facilityRepository)
        {
            _metricMigrationExtractRepository = metricMigrationExtractRepository;
            _facilityRepository = facilityRepository;
        }

        public void Process(IEnumerable<MetricMigrationExtract> metricMigrationExtracts)
        {
            _siteProfiles = _facilityRepository.GetSiteProfiles().ToList();

            var batch = new List<MetricMigrationExtract>();
            int count = 0;

            foreach (var metricMigrationExtract in metricMigrationExtracts)
            {
                count++;
                try
                {
                    metricMigrationExtract.FacilityId = GetFacilityId(metricMigrationExtract.SiteCode);
                    batch.Add(metricMigrationExtract);
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Facility Id missing {metricMigrationExtract.SiteCode}");
                }


                if (count == 1000)
                {
                    _metricMigrationExtractRepository.CreateBulk(batch);
                    count = 0;
                    batch = new List<MetricMigrationExtract>();
                }

            }

            if (batch.Any())
                _metricMigrationExtractRepository.CreateBulk(batch);



        }

        public Guid GetFacilityId(int siteCode)
        {
            var profile = _siteProfiles.FirstOrDefault(x => x.SiteCode == siteCode);
            if (null == profile)
                throw new FacilityNotFoundException(siteCode);

            return profile.FacilityId;
        }
    }
}
