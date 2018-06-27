using System;
using System.Collections.Generic;
using DwapiCentral.Cbs.Core.Command;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.DTOs;
using DwapiCentral.SharedKernel.Interfaces;

namespace DwapiCentral.Cbs.Core.Interfaces.Repository
{
    public interface IMasterPatientIndexRepository : IRepository<MasterPatientIndex,Guid>
    {
        void Process(Guid facilityId,IEnumerable<MasterPatientIndex> masterPatientIndices);
        List<MpiSearchResultDto> MpiSearch(SearchMpi parameters);
    }
}