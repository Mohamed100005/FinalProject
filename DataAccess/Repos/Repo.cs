using DataAccess.Repos.IRepos;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repos
{
    public class Repo<T> : IRepo<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;
        DbSet<T> dbset;
        public Repo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbset = dbContext.Set<T>();
        }

        public void Create(T entity)
        {
            dbset.Add(entity);
        }
        public void CreateAll(List<T> entities)
        {
            dbset.AddRange(entities);
        }
        public void Delete(T entity)
        {
            dbset.Remove(entity);
        }
        public void DeleteAll(List<T> entities)
        {
            dbset.RemoveRange(entities);
        }
        public void Edit(T entity)
        {
            dbset.Update(entity);
        }
        public void EditAll(List<T> entities)
        {
            dbset.UpdateRange(entities);
        }
        public void Attemp()
        {
            dbContext.SaveChanges();
        }
        public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includation = null, bool tracked = true)
        {
            IQueryable<T> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includation != null)
            {
                foreach (var item in includation)
                {
                    query = query.Include(item);
                }
            }
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return query;
        }
        public T GetOne(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includation = null, bool tracked = true)
        {
            return Get(filter, includation, tracked).FirstOrDefault();
        }
    }
}
