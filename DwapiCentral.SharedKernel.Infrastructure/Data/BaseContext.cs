using Microsoft.EntityFrameworkCore;

namespace DwapiCentral.SharedKernel.Infrastructure.Data
{
    public abstract class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions options) : base(options)
        {
        }

        public virtual void EnsureSeeded()
        {
            
        }
    }
}
