using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Enums;
using DwapiCentral.SharedKernel.Infrastructure.Data;

namespace DwapiCentral.Cbs.Infrastructure.Data.Repository
{
    public class ManifestRepository : BaseRepository<Manifest, Guid>, IManifestRepository
    {
        public ManifestRepository(CbsContext context) : base(context)
        {
        }

        public async void Process()
        {
            var manifests =GetAll(x => x.Status == ManifestStatus.Staged).ToList();
            if (manifests.Any())
                await ClearFacility(manifests);
        }

        public Task<int> ClearFacility(IEnumerable<Manifest> manifests)
        {
            var ids = string.Join(',', manifests.Select(x =>$"'{x.FacilityId}'"));
            return ExecSqlAsync($"DELETE FROM {nameof(CbsContext.MasterPatientIndices)} WHERE {nameof(MasterPatientIndex.FacilityId)} in ({ids})");
        }
    }
}