using System;
using DwapiCentral.SharedKernel;
using DwapiCentral.SharedKernel.Model;

namespace DwapiCentral.Cbs.Core.Model
{
    public class MasterPatientIndex:Entity<Guid>
    {
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public Guid FacilityId { get; set; }
    }
}