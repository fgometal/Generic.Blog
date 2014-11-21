using System.Collections.Generic;
using System.Linq;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Repository;
using Ninject;

namespace MundiPagg.Blog.Service
{
    /// <summary>
    /// Classe de serviços para a entidade PostCommentary
    /// </summary>
    public class PostCommentaryService
    {
        /// <summary>
        /// Realização da injeção do repositório na instância da classe.
        /// </summary>
        [Inject]
        public PostCommentaryRepository Repository { get; set; }
        /// <summary>
        /// Obtém todos os comentários.
        /// </summary>
        /// <returns>Uma lista de comentários</returns>
        public List<PostCommentary> GetAll()
        {
            return Repository.FetchAll.ToList();
        }
        /// <summary>
        /// Obtém um comentário por id
        /// </summary>
        /// <param name="id">Id do comentário</param>
        /// <returns>Um objeto PostCommentary</returns>
        public PostCommentary GetById(int id)
        {
            return Repository.GetById(id);
        }
        /// <summary>
        /// Adiciona e salva um comentário na base.
        /// </summary>
        /// <param name="commentary">O objeto PostCommentary a ser adicionado</param>
        public void Save(PostCommentary commentary)
        {
            Repository.Add(commentary);
            Repository.Save();
        }
        /// <summary>
        /// Marca a alteração de estado e salva um comentário na base.
        /// </summary>
        /// <param name="commentary">O objeto user a ser alterado / editado</param>
        public void Update(PostCommentary commentary)
        {
            Repository.Edit(commentary);
            Repository.Save();
        }
        /// <summary>
        /// Remove um comentário da base de dados e salva a base.
        /// </summary>
        /// <param name="commentary">O objeto a ser excluído</param>
        public void Delete(PostCommentary commentary)
        {
            Repository.Delete(commentary);
            Repository.Save();
        }
    }
}
