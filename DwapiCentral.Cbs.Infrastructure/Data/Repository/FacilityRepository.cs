using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DwapiCentral.Cbs.Core.Exchange;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Infrastructure.Data;
using DwapiCentral.SharedKernel.Model;
using Serilog;

namespace DwapiCentral.Cbs.Infrastructure.Data.Repository
{
    public class FacilityRepository : BaseRepository<Facility, Guid>, IFacilityRepository
    {
        public FacilityRepository(CbsContext context) : base(context)
        {
        }

        public IEnumerable<SiteProfile> GetSiteProfiles()
        {
            return GetAll().Select(x => new SiteProfile(x.SiteCode, x.Id));
        }

        public IEnumerable<SiteProfile> GetSiteProfiles(List<int> siteCodes)
        {
            return GetAll(x=>siteCodes.Contains(x.SiteCode)).Select(x => new SiteProfile(x.SiteCode, x.Id));
        }

          public IEnumerable<StatsDto> GetFacStats(IEnumerable<Guid> facilityIds)
        {
            var list = new List<StatsDto>();
            foreach (var facilityId in facilityIds)
            {
                try
                {
                    var stat = GetFacStats(facilityId);
                    if(null!=stat)
                        list.Add(stat);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }


            }
            return list;
        }

        public StatsDto GetFacStats(Guid facilityId)
        {
            string sql = $@"
select
(select top 1 SiteCode from Facilities where id='{facilityId}') FacilityCode,
(select max(DateCreated) from MasterPatientIndices where facilityid='{facilityId}') Updated,
(select count(id) from MasterPatientIndices where facilityid='{facilityId}') MasterPatientIndex
                ";

            var result = GetDbConnection().Query<dynamic>(sql).FirstOrDefault();

            if (null != result)
            {
                var stats=new StatsDto(result.FacilityCode,result.Updated,"MPI");
                stats.AddStats("MasterPatientIndex",result.MasterPatientIndex);
                return stats;
            }

            return null;
        }

        public StatsDto GetFacMetricStats(Guid facilityId)
        {
            string sql = $@"
select
(select top 1 SiteCode from Facilities where id='{facilityId}') FacilityCode,
(select max(DateCreated) from MetricMigrationExtracts where facilityid='{facilityId}') Updated,
(select count(id) from MetricMigrationExtracts where Metric='persons' and facilityid='{facilityId}') Persons
                ";

            var result = GetDbConnection().Query<dynamic>(sql).FirstOrDefault();

            if (null != result)
            {
                var stats=new StatsDto(result.FacilityCode,result.Updated,"MGS");
                stats.AddStats("Persons",result.Persons);

                return stats;
            }

            return null;
        }
    }
}
