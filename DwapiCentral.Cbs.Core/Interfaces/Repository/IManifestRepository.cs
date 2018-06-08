using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Interfaces;

namespace DwapiCentral.Cbs.Core.Interfaces.Repository
{
    public interface IManifestRepository : IRepository<Manifest, Guid>
    {
        Task<int> ClearFacility(IEnumerable<Manifest> manifests);
    }
}