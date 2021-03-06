using System.Threading.Tasks;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Interfaces;

namespace DwapiCentral.Cbs.Core.Interfaces.Repository
{
    public interface IDocketRepository : IRepository<Docket, string>
    {
       Task<Docket> FindAsync(string docket);
    }
}