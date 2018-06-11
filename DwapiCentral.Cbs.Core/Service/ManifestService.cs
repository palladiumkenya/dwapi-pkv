using System.Linq;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Interfaces.Service;
using DwapiCentral.SharedKernel.Enums;

namespace DwapiCentral.Cbs.Core.Service
{
    public class ManifestService:IManifestService
    {
        private readonly IManifestRepository _manifestRepository;

        public ManifestService(IManifestRepository manifestRepository)
        {
            _manifestRepository = manifestRepository;
        }

        public void Process()
        {
            var manifests = _manifestRepository.GetAll(x => x.Status == ManifestStatus.Staged).ToList();
            if (manifests.Any())
                _manifestRepository.ClearFacility(manifests);
        }
    }
}