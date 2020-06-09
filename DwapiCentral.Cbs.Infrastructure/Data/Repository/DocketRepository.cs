using System.Threading.Tasks;
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
        public Task<Docket> FindAsync(string docket)
        {
           var ctx=Context as CbsContext;

           var dockeT= ctx.Dockets.Include(x => x.Subscribers)
               .AsTracking()
               .FirstOrDefaultAsync(x => x.Id == docket);

           return dockeT;
        }
    }
}
