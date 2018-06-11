using System.Collections.Generic;
using DwapiCentral.Cbs.Core.Model;

namespace DwapiCentral.Cbs.Core.Interfaces.Service
{
    public interface IMpiService
    {
        void Process(IEnumerable<MasterPatientIndex> masterPatientIndices);
    }
}