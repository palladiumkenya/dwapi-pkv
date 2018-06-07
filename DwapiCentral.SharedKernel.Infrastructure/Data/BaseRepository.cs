using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public virtual T  Get(TId id)
        {
            var entity= DbSet.Find(id);
            return entity;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DbSet.AsNoTracking();
        }
        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }
        public int GetCount()
        {
            return GetAll().Select(x => x.Id).Count();
        }

        public bool EntityExists(T entity)
        {
            if (null == entity)
                return false;

            var criteria = Get(entity.Id);

            return null != criteria;
        }

        public bool Exists(T entity)
        {
            if (null == entity)
                return false;

            var criteria = DbSet.AsNoTracking().FirstOrDefault(x => x.Id.Equals(entity.Id));

            return null != criteria;
        }


        public bool EntityExists(T entity, Expression<Func<T, bool>> predicate, bool excluedSelf = true)
        {
            if (null == entity)
                return false;

            var criteria = GetAll(predicate);

            if (excluedSelf)
                return criteria.Any(x => !x.Id.Equals(entity.Id));

            return criteria.Any();
        }

        public virtual void Create(T entity)
        {
            if (null != entity)
            {
                Context.Add(entity);
            }
        }

        public virtual void Update(T entity)
        {
            if (null != entity)
            {
                Context.Update(entity);
            }
        }

        public void CreateOrUpdate(T entity)
        {
            if (EntityExists(entity))
            {
                Update(entity);
            }
            else
            {
                Create(entity);
            }
        }

        public void SaveOrUpdate(T entity)
        {
            if (Exists(entity))
            {
                Update(entity);
            }
            else
            {
                Create(entity);
            }
        }

        public virtual void Delete(TId id)
        {
            var entity = DbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(T entity)
        {
            if (null != entity)
                DbSet.Remove(entity);
        }

        public virtual void Save()
        {
            Context.SaveChanges();
        }
    }
}