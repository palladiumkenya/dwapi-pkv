using System;
using System.Linq;
using DwapiCentral.Cbs.Core.Interfaces;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DwapiCentral.Cbs.Infrastructure.Data.Repository
{
    public class FacilityRepository : BaseRepository<Facility, Guid>, IFacilityRepository
    {
        public FacilityRepository(CbsContext context) : base(context)
        {
        }

        public bool IsEnrolled(int siteCode)
        {
            return GetAll(x=>x.SiteCode==siteCode).Any();
        }
    }
}