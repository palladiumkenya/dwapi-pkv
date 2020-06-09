using System;
using System.Collections.Generic;
using DwapiCentral.Cbs.Core.Model;
using MediatR;

namespace DwapiCentral.Cbs.Core.Command
{
    public class SaveMgs : IRequest<Guid>
    {
        public IEnumerable<MetricMigrationExtract> Migrations { get; set; }

        public SaveMgs( IEnumerable<MetricMigrationExtract> metricMigrationExtracts)
        {
            Migrations = metricMigrationExtracts;
        }
    }
}
