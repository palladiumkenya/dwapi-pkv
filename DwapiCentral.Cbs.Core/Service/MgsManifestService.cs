using System;
using System.Linq;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Interfaces.Service;
using DwapiCentral.Cbs.Core.Model.Dto;
using DwapiCentral.SharedKernel.Enums;
using Serilog;

namespace DwapiCentral.Cbs.Core.Service
{
    public class MgsManifestService : IMgsManifestService
    {
        private readonly IManifestRepository _manifestRepository;
        private readonly IMasterFacilityRepository _masterFacilityRepository;
        private readonly ILiveSyncService _liveSyncService;

        public MgsManifestService(IManifestRepository manifestRepository,
            IMasterFacilityRepository masterFacilityRepository, ILiveSyncService liveSyncService)
        {
            _manifestRepository = manifestRepository;
            _masterFacilityRepository = masterFacilityRepository;
            _liveSyncService = liveSyncService;
        }

        public void Process(bool sync = true)
        {
            var manifests = _manifestRepository.GetStagedMetrics().ToList();
            if (manifests.Any())
            {
                try
                {
                    _manifestRepository.ClearFacilityMetrics(manifests);
                }
                catch (Exception e)
                {
                    Log.Error("Clear MANIFEST ERROR ", e);
                }

                foreach (var manifest in manifests)
                {
                    var clientCount = _manifestRepository.GetPatientCount(manifest.Id);
                    if (sync)
                        _liveSyncService.SyncManifest(manifest, clientCount, "MGS");

                    try
                    {
                        // Get MasterFacility
                        var masterFacility = _masterFacilityRepository.GetBySiteCode(manifest.SiteCode);

                        if (null != masterFacility)
                        {
                            // Sync Metrics
                            var metricDtos = MetricDto.Generate(masterFacility, manifest);
                            if (sync)
                                _liveSyncService.SyncMetrics(metricDtos);
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message);
                    }

                }

            }
        }
    }
}
