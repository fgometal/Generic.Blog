using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MundiPagg.Blog.Repository.Interfaces;
using MundiPagg.Blog.Repository.DatabaseContext;

namespace MundiPagg.Blog.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public readonly BlogDBContext DatabaseContext;
        
        public BaseRepository()
        {
            DatabaseContext = DbInstance.Instance.Context;
        }

        public IQueryable<T> FetchAll
        {
            get { return DatabaseContext.Set<T>(); }
        }

        public T GetById(int id)
        {
            return DatabaseContext.Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            DatabaseContext.Set<T>().Add(entity);
        }

        public void Edit(T entity)
        {
            DatabaseContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            DatabaseContext.Set<T>().Remove(entity);
        }

        public void Save()
        {
            DatabaseContext.SaveChanges();
        }
    }
}
