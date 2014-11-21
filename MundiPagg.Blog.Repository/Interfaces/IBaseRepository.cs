using System.Linq;

namespace MundiPagg.Blog.Repository.Interfaces
{
    /// <summary>
    /// Interface para o repositório genérico.
    /// Define um contrato de métodos básicos que todo repositório
    /// instanciado deve conter.
    /// </summary>
    /// <typeparam name="T">A entidade de domínio que define o
    /// tipo de repositório a ser criado.</typeparam>
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
