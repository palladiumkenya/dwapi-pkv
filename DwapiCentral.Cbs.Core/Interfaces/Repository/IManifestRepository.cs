using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.Cbs.Core.Model.Dto;
using DwapiCentral.SharedKernel.Interfaces;

namespace DwapiCentral.Cbs.Core.Interfaces.Repository
{
    public interface IManifestRepository : IRepository<Manifest, Guid>
    {
        void ClearFacility(IEnumerable<Manifest> manifests);
        void ClearFacilityMetrics(IEnumerable<Manifest> manifests);
        int GetPatientCount(Guid id);
        IEnumerable<Manifest> GetStaged();
        IEnumerable<Manifest> GetStagedMetrics();
        Task EndSession(Guid session);
        IEnumerable<HandshakeDto> GetSessionHandshakes(Guid session);
    }
}
