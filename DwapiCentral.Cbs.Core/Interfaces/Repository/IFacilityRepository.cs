using System;
using System.Collections.Generic;
using DwapiCentral.Cbs.Core.Exchange;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Interfaces;
using DwapiCentral.SharedKernel.Model;

namespace DwapiCentral.Cbs.Core.Interfaces.Repository
{
    public interface IFacilityRepository : IRepository<Facility, Guid>
    {
        IEnumerable<SiteProfile> GetSiteProfiles();
        IEnumerable<SiteProfile> GetSiteProfiles(List<int> siteCodes);
        IEnumerable<StatsDto> GetFacStats(IEnumerable<Guid> facilityIds);
        IEnumerable<StatsDto> GetFacMgsStats(IEnumerable<Guid> facilityIds);
        StatsDto GetFacStats(Guid facilityId);
        StatsDto GetFacMetricStats(Guid facilityId);
        Facility GetBySiteCode(int siteCode);
    }
}
