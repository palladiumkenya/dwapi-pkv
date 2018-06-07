using DwapiCentral.Cbs.Core.Interfaces;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DwapiCentral.Cbs.Infrastructure.Data.Repository
{
    public class MasterFacilityRepository:BaseRepository<MasterFacility,int>, IMasterFacilityRepository
    {
        public MasterFacilityRepository(CbsContext context) : base(context)
        {
        }
    }
}