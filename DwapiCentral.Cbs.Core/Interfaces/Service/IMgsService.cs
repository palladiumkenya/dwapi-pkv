using System.Collections.Generic;
using DwapiCentral.Cbs.Core.Model;

namespace DwapiCentral.Cbs.Core.Interfaces.Service
{
    public interface IMgsService
    {
        void Process(IEnumerable<MetricMigrationExtract> metricMigrationExtracts, bool sync = true);
    }
}
