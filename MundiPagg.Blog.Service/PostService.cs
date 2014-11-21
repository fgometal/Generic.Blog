using System.Collections.Generic;
using System.Linq;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Repository;
using Ninject;

namespace MundiPagg.Blog.Service
{
    /// <summary>
    /// Classe de serviços para a entidade Post
    /// </summary>
    public class PostService
    {
        /// <summary>
        /// Realização da injeção do repositório na instância da classe.
        /// </summary>
        [Inject]
        public PostRepository Repository { get; set; }
        /// <summary>
        /// Obtém todos as postagens.
        /// </summary>
        /// <returns>Uma lista de postagens</returns>
        public List<Post> GetAll()
        {
            return Repository.FetchAll.ToList();
        }
        /// <summary>
        /// Obtém uma postagem por id
        /// </summary>
        /// <param name="id">Id da postagem</param>
        /// <returns>Um objeto Post</returns>
        public Post GetById(int id)
        {
            return Repository.GetById(id);
        }
        /// <summary>
        /// Adiciona e salva uma postagem na base.
        /// </summary>
        /// <param name="post">O objeto Post a ser adicionado</param>
        public void Save(Post post)
        {
            Repository.Add(post);
            Repository.Save();
        }
        /// <summary>
        /// Marca a alteração de estado e salva uma postagem na base.
        /// </summary>
        /// <param name="post">O objeto Post a ser alterado / editado</param>
        public void Update(Post post)
        {
            Repository.Edit(post);
            Repository.Save();
        }
        /// <summary>
        /// Remove uma postagem da base de dados e salva a base.
        /// </summary>
        /// <param name="post">O objeto a ser excluído</param>
        public void Delete(Post post)
        {
            Repository.Delete(post);
            Repository.Save();
        }
        /// <summary>
        /// Obtém as postagens por paginação, ou seja, são passados
        /// um índice e um comprimento de lista para se obter grupos de 
        /// postagens por consulta.
        /// </summary>
        /// <param name="page">Índice do início da consulta</param>
        /// <param name="pageSize">Comprimento da lista de retorno.</param>
        /// <returns>Uma lista de Posts ordenanada por data em ordem decrescente.</returns>
        public List<Post> GetPostsPaginated(int page, int pageSize)
        {
            return GetAll()
                .OrderByDescending(post => post.PublishDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
        /// <summary>
        /// Obtém as postagens por paginação aonde os posts devem
        /// pertencer a um usuário específico.
        /// </summary>
        /// <param name="page">Índice do início da consulta</param>
        /// <param name="pageSize">Comprimento da lista de retorno.</param>
        /// <param name="userId">Id do usuário autor dos posts.</param>
        /// <returns>Uma lista de posts ordenadas por data decrescente
        /// do usuário especificado.</returns>
        public List<Post> GetPostsByUserId(int page, int pageSize, int userId)
        {
            return GetPostsPaginated(page, pageSize)
                .Where(post => post.User.UserId == userId)
                .ToList();
        }
    }
}
