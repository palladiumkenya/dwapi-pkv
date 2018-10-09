using System;
using System.Collections.Generic;
using System.Linq;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Interfaces.Service;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Enums;
using DwapiCentral.SharedKernel.Exceptions;
using DwapiCentral.SharedKernel.Model;
using Serilog;

namespace DwapiCentral.Cbs.Core.Service
{
    public class MpiService : IMpiService
    {
        private readonly IMasterPatientIndexRepository _manifestRepository;
        private readonly IFacilityRepository _facilityRepository;
        private List<SiteProfile> _siteProfiles = new List<SiteProfile>();

        public MpiService(IMasterPatientIndexRepository manifestRepository, IFacilityRepository facilityRepository)
        {
            _manifestRepository = manifestRepository;
            _facilityRepository = facilityRepository;
        }

        public void Process(IEnumerable<MasterPatientIndex> masterPatientIndices)
        {
            _siteProfiles = _facilityRepository.GetSiteProfiles().ToList();

            var batch = new List<MasterPatientIndex>();
            int count = 0;

            foreach (var masterPatientIndex in masterPatientIndices)
            {
                try
                {
                    masterPatientIndex.FacilityId = GetFacilityId(masterPatientIndex.SiteCode);
                    batch.Add(masterPatientIndex);
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Facility Id missing {masterPatientIndex.SiteCode}");
                }


                if (count == 1000)
                {
                    _manifestRepository.CreateBulk(batch);
                    count = 0;
                    batch = new List<MasterPatientIndex>();
                }

            }

            if (batch.Any())
                _manifestRepository.CreateBulk(batch);



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
