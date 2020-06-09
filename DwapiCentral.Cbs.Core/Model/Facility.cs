using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DwapiCentral.SharedKernel.Model;
using DwapiCentral.SharedKernel.Utils;

namespace DwapiCentral.Cbs.Core.Model
{
    public class Facility : Entity<Guid>
    {
        public int SiteCode { get; set; }
        [MaxLength(120)] public string Name { get; set; }
        public int? MasterFacilityId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string Emr { get; set; }
        public DateTime? SnapshotDate { get; set; }
        public int? SnapshotSiteCode { get; set; }
        public int? SnapshotVersion { get; set; }

        public ICollection<MasterPatientIndex> MasterPatientIndices { get; set; } = new List<MasterPatientIndex>();

        public ICollection<MetricMigrationExtract> MetricMigrationExtracts { get; set; } =
            new List<MetricMigrationExtract>();

        public ICollection<Manifest> Manifests { get; set; } = new List<Manifest>();

        public Facility()
        {
        }

        public Facility(int siteCode, string name)
        {
            SiteCode = siteCode;
            Name = name;
        }

        public Facility(int siteCode, string name, int? masterFacilityId) : this(siteCode, name)
        {
            MasterFacilityId = masterFacilityId;
        }

        public override string ToString()
        {
            return $"{Name} - {SiteCode}";
        }


        public bool EmrChanged(string requestEmr)
        {
            if (string.IsNullOrWhiteSpace(requestEmr))
                return false;

            if (string.IsNullOrWhiteSpace(Emr))
                return false;

            if (requestEmr.IsSameAs("CHAK"))
                requestEmr = "IQCare";

            if (requestEmr.IsSameAs("IQCare") || requestEmr.IsSameAs("KenyaEMR"))
                return !Emr.IsSameAs(requestEmr);

            return false;
        }

        public Facility TakeSnapFrom(MasterFacility snapMfl)
        {
            var fac = this;

            fac.SnapshotDate = DateTime.Now;
            fac.SiteCode = snapMfl.Id;
            fac.SnapshotSiteCode = snapMfl.SnapshotSiteCode;
            fac.SnapshotVersion = snapMfl.SnapshotVersion;

            return fac;
        }
    }
}
