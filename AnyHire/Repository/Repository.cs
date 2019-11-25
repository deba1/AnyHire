using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AnyHire.Interface;
using AnyHire.Models;
using System.Data.Entity;

namespace AnyHire.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private AnyHireDbContext context = new AnyHireDbContext();
        public Repository(AnyHireDbContext context)
        {
            this.context = context;
        }

        public TEntity GetById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public void Insert(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }
        public int Submit()
        {
            return context.SaveChanges();
        }
        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}