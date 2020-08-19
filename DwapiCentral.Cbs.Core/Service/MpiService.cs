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
    public class MpiService : IMpiService
    {
        private readonly IMasterPatientIndexRepository _manifestRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly ILiveSyncService _syncService;
        private List<SiteProfile> _siteProfiles = new List<SiteProfile>();

        public MpiService(IMasterPatientIndexRepository manifestRepository, IFacilityRepository facilityRepository, ILiveSyncService syncService)
        {
            _manifestRepository = manifestRepository;
            _facilityRepository = facilityRepository;
            _syncService = syncService;
        }

        public void Process(IEnumerable<MasterPatientIndex> masterPatientIndices, bool sync = true)
        {
            List<Guid> facilityIds = new List<Guid>();

            if (null == masterPatientIndices)
                return;
            if (!masterPatientIndices.Any())
                return;

            _siteProfiles = _facilityRepository.GetSiteProfiles().ToList();

            var batch = new List<MasterPatientIndex>();
            int count = 0;

            foreach (var masterPatientIndex in masterPatientIndices)
            {
                count++;
                try
                {
                    masterPatientIndex.UpdateRef();
                    masterPatientIndex.FacilityId = GetFacilityId(masterPatientIndex.SiteCode);
                    batch.Add(masterPatientIndex);
                    facilityIds.Add(masterPatientIndex.FacilityId);
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

            if (sync)
                SyncClients(facilityIds);

        }

        public Guid GetFacilityId(int siteCode)
        {
            var profile = _siteProfiles.FirstOrDefault(x => x.SiteCode == siteCode);
            if (null == profile)
                throw new FacilityNotFoundException(siteCode);

            return profile.FacilityId;
        }

        private void SyncClients(List<Guid> facIlds)
        {
            if (facIlds.Any())
            {
                _syncService.SyncStats(facIlds.Distinct().ToList());
            }
        }
    }
}
