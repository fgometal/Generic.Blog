using System.Linq;
using System.Data.Entity;
using MundiPagg.Blog.Repository.Interfaces;
using MundiPagg.Blog.Repository.DatabaseContext;

namespace MundiPagg.Blog.Repository
{
    /// <summary>
    /// Classe de repositório genérico aonde são definidos os métodos básicos de 
    /// acesso a base que serão herdados pelos repositórios descendentes.
    /// Herda de IBaseRepository aonde temos um contrato do que um repositório deve conter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Instância do banco para uso interno do repositório.
        /// </summary>
        public readonly BlogDBContext DatabaseContext;
        /// <summary>
        /// Construtor que obtém a instância do banco e a passa
        /// para o campo de uso interno da classe.
        /// </summary>
        public BaseRepository()
        {
            DatabaseContext = DbInstance.Instance.Context;
        }
        /// <summary>
        /// Obtém todos os itens.
        /// </summary>
        public IQueryable<T> FetchAll
        {
            get { return DatabaseContext.Set<T>(); }
        }
        /// <summary>
        /// Obtém um item por id
        /// </summary>
        /// <param name="id">id do item desejado</param>
        /// <returns>O item encontrado pela id.</returns>
        public T GetById(int id)
        {
            return DatabaseContext.Set<T>().Find(id);
        }
        /// <summary>
        /// Adiciona um novo item à instância do banco.
        /// </summary>
        /// <param name="entity">Item a ser adicionado</param>
        public void Add(T entity)
        {
            DatabaseContext.Set<T>().Add(entity);
        }
        /// <summary>
        /// Altera o estado de mudança do item passado
        /// </summary>
        /// <param name="entity">Item a ser alterado.</param>
        public void Edit(T entity)
        {
            DatabaseContext.Entry(entity).State = EntityState.Modified;
        }
        /// <summary>
        /// Remove um item da instância do banco.
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            DatabaseContext.Set<T>().Remove(entity);
        }
        /// <summary>
        /// Efetua as mudanças ocorridas na instância do banco.
        /// </summary>
        public void Save()
        {
            DatabaseContext.SaveChanges();
        }
    }
}
