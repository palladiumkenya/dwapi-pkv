using System;
using DwapiCentral.SharedKernel.Model;

namespace DwapiCentral.Cbs.Core.Model
{
    public class MetricMigrationExtract : Entity<Guid>
    {
        public  int MetricId { get; set; }
        public int SiteCode { get; set; }
        public string Dataset { get; set; }
        public string Metric { get; set; }
        public string MetricValue { get; set; }
        public DateTime? CreateDate { get; set; }
        public virtual string Emr { get; set; }
        public virtual string Project { get; set; }
        public virtual bool? Processed { get; set; }
        public virtual string QueueId { get; set; }
        public virtual string Status { get; set; }
        public virtual DateTime? StatusDate { get; set; }
        public virtual DateTime? DateExtracted { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public Guid FacilityId { get; set; }

        public override string ToString()
        {
            return $"{Dataset} | {Metric} | {MetricValue}";
        }
    }
}
