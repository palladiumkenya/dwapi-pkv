using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DwapiCentral.SharedKernel.Interfaces;
using DwapiCentral.SharedKernel.Model;
using Microsoft.EntityFrameworkCore;

namespace DwapiCentral.SharedKernel.Infrastructure.Data
{
    public abstract class BaseRepository<T, TId> : IRepository<T, TId> where T : Entity<TId>
    {
        protected internal DbContext Context;
        protected internal DbSet<T> DbSet;

        protected BaseRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public string ConnectionString => GetDbConnection().ConnectionString;

        public Task<T> GetAsync(TId id)
        {
            return DbSet.FindAsync(id);
        }

        public void Create(T entity)
        {
            DbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return GetAll(predicate).FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).AsNoTracking();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public IDbConnection GetDbConnection()
        {
            return Context.Database.GetDbConnection();
        }
    }
}