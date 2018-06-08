using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DwapiCentral.Cbs.Core.Interfaces;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DwapiCentral.Cbs.Infrastructure.Data.Repository
{
    public class DocketRepository : BaseRepository<Docket, string>, IDocketRepository
    {
        public DocketRepository(CbsContext context) : base(context)
        {
        }
    }
}