using LogManager.Business.Interfaces;
using LogManager.Business.Models;
using LogManager.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace LogManager.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly LogManagerContext Context;
        protected readonly DbSet<TEntity> DbSet;
        
        public Repository(LogManagerContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public TEntity Read(Guid id)
        {
            return DbSet.Find(id);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public void Delete(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public IEnumerable<TEntity> List()
        {
            return DbSet.AsQueryable();
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
