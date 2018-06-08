using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Process(Guid facilityId,IEnumerable<MasterPatientIndex> masterPatientIndices)
        {
            var mpi = masterPatientIndices.ToList();

            if (mpi.Any())
            {
                mpi.ForEach(x => x.FacilityId = facilityId);
                CreateBulk(mpi);
            }
        }
    }
}