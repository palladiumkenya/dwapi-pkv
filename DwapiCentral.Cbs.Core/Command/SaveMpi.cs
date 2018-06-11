using System;
using System.Collections.Generic;
using DwapiCentral.Cbs.Core.Model;
using MediatR;

namespace DwapiCentral.Cbs.Core.Command
{
    public class SaveMpi : IRequest<Guid>
    {
        public IEnumerable<MasterPatientIndex> MasterPatientIndices { get; set; }

        public SaveMpi( IEnumerable<MasterPatientIndex> masterPatientIndices)
        {
  
            MasterPatientIndices = masterPatientIndices;
        }
    }
}