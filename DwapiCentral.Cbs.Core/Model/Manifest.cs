using System;
using System.Collections.Generic;
using DwapiCentral.SharedKernel.Enums;
using DwapiCentral.SharedKernel.Model;

namespace DwapiCentral.Cbs.Core.Model
{
    public class Manifest : Entity<Guid>
    {
        public int SiteCode { get; set; }
        public string Name { get; set; }
        public int Sent { get; set; }
        public int Recieved { get; set; }
        public DateTime DateLogged { get; set; }
        public DateTime DateArrived { get; set; } = DateTime.Now;
        public ManifestStatus Status { get; set; }
        public DateTime StatusDate { get; set; } = DateTime.Now;

        public ManifestType ManifestType { get; set; }
        public Guid FacilityId { get; set; }
        public ICollection<Cargo> Cargoes { get; set; } = new List<Cargo>();

        public Manifest()
        {
        }

        public void UpdateFacility(Guid facilityId)
        {
            FacilityId = facilityId;
        }
    }
}
