using System;
using System.Collections.Generic;
using DwapiCentral.Cbs.Core.Interfaces;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DwapiCentral.Cbs.Infrastructure.Data.Repository
{
    public class MasterPatientIndexRepository : BaseRepository<MasterPatientIndex,Guid>, IMasterPatientIndexRepository
    {
        public MasterPatientIndexRepository(CbsContext context) : base(context)
        {
        }
    }
}