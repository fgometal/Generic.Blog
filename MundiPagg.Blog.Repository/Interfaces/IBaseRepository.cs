using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Blog.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> FetchAll { get; }
        T GetById(int id);
        void Add(T entity);
        void Edit(T entity);
        void Delete(T entity);
        void Save();
    }
}
