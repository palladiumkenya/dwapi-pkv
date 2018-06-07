using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DwapiCentral.SharedKernel.Model;

namespace DwapiCentral.SharedKernel.Interfaces
{
    public interface IRepository<T, in TId> where T : Entity<TId>
    {
        T Get(TId id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        int GetCount();
        bool EntityExists(T entity);
        bool Exists(T entity);
        bool EntityExists(T entity, Expression<Func<T, bool>> predicate, bool excluedSelf = true);
        void Create(T entity);
        void Update(T entity);
        void CreateOrUpdate(T entity);
        void SaveOrUpdate(T entity);
        void Delete(TId id);
        void Delete(T entity);
        void Save();
    }
}