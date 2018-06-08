using System;
using System.Collections.Generic;
using DwapiCentral.Cbs.Core.Model;
using MediatR;

namespace DwapiCentral.Cbs.Core.Command
{
    public class SaveMpi : IRequest<Guid>
    {
        public Guid FacilityId { get;  }
        public IEnumerable<MasterPatientIndex> MasterPatientIndices { get; }

        public SaveMpi(Guid facilityId, IEnumerable<MasterPatientIndex> masterPatientIndices)
        {
            FacilityId = facilityId;
            MasterPatientIndices = masterPatientIndices;
        }
    }
}