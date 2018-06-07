using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DwapiCentral.SharedKernel;
using DwapiCentral.SharedKernel.Model;

namespace DwapiCentral.Cbs.Core.Model
{
    public class Facility : Entity<Guid>
    {
        public int SiteCode { get; set; }
        [MaxLength(150)] public string Name { get; set; }
        public int? MasterFacilityId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public ICollection<MasterPatientIndex> MasterPatientIndices { get; set; }=new List<MasterPatientIndex>();
        public ICollection<FacilityManifest> Manifests { get; set; }=new List<FacilityManifest>();
    }
}