using System;
using DwapiCentral.SharedKernel;
using DwapiCentral.SharedKernel.Model;

namespace DwapiCentral.Cbs.Core.Model
{
    public class FacilityManifest:Entity<Guid>
    {
        public int AmountSent { get; set; }
        public int AmountRecievied { get; set; }
        public Guid FacilityId { get; set; }
        public DateTime DateLogged { get; set; }=DateTime.Now;

        public FacilityManifest()
        {
        }
    }
}