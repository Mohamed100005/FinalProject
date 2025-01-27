using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repos.IRepos
{
    public interface IRepo<T> where T : class
    {
        public void Create(T entity);
        public void CreateAll(List<T> entities);
        public void Delete(T entity);
        public void DeleteAll(List<T> entities);
        public void Edit(T entity);
        public void EditAll(List<T> entities);
        public void Attemp();
        public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includation = null, bool tracked = true);
        public T GetOne(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includation = null, bool tracked = true);
    }
}
