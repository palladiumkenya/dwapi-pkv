using System.Linq;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Interfaces.Service;
using DwapiCentral.SharedKernel.Enums;

namespace DwapiCentral.Cbs.Core.Service
{
    public class MgsManifestService:IMgsManifestService
    {
        private readonly IManifestRepository _manifestRepository;

        public MgsManifestService(IManifestRepository manifestRepository)
        {
            _manifestRepository = manifestRepository;
        }

        public void Process()
        {
            var manifests = _manifestRepository.GetAll(x => x.Status == ManifestStatus.Staged).ToList();
            if (manifests.Any())
            {
                _manifestRepository.ClearFacilityMetrics(manifests);
            }
        }
    }
}
