using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DwapiCentral.SharedKernel.Model;

namespace DwapiCentral.SharedKernel.Interfaces
{
    public interface IRepository<T, in TId> where T : Entity<TId>
    {
        string ConnectionString { get; }
        Task<T> GetAsync(TId id);
        void Create(T entity);
        T Get(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        void Save();
        IDbConnection GetDbConnection();
    }
}