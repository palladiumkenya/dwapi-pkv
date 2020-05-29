using System.Collections.Generic;
using System.Linq;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Infrastructure.Data;

namespace DwapiCentral.Cbs.Infrastructure.Data.Repository
{
    public class MasterFacilityRepository:BaseRepository<MasterFacility,int>, IMasterFacilityRepository
    {
        public MasterFacilityRepository(CbsContext context) : base(context)
        {
        }

        public MasterFacility GetBySiteCode(int siteCode)
        {
            return DbSet.Find(siteCode);
        }

        public List<MasterFacility> GetLastSnapshots(int siteCode)
        {
            return DbSet.Where(x =>  x.SnapshotSiteCode == siteCode)
                .ToList();
        }
    }
}
