using System;
using System.Collections.Generic;
using System.Linq;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Infrastructure.Data;

namespace DwapiCentral.Cbs.Infrastructure.Data.Repository
{
    public class MetricMigrationExtractRepository : BaseRepository<MetricMigrationExtract,Guid>, IMetricMigrationExtractRepository
    {
        public MetricMigrationExtractRepository(CbsContext context) : base(context)
        {
        }

        public void Process(Guid facilityId,IEnumerable<MetricMigrationExtract> metricMigrationExtracts)
        {
            var mpi = metricMigrationExtracts.ToList();

            if (mpi.Any())
            {
                mpi.ForEach(x => x.FacilityId = facilityId);
                CreateBulk(mpi);
            }
        }
    }
}
