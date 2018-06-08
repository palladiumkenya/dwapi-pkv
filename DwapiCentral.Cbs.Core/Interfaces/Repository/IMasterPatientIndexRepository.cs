using System;
using System.Collections.Generic;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Interfaces;

namespace DwapiCentral.Cbs.Core.Interfaces.Repository
{
    public interface IMasterPatientIndexRepository : IRepository<MasterPatientIndex,Guid>
    {
        void Process(Guid facilityId,IEnumerable<MasterPatientIndex> masterPatientIndices);
    }
}