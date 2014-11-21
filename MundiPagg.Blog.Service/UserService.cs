using System.Collections.Generic;
using System.Linq;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Repository;
using Ninject;

namespace MundiPagg.Blog.Service
{
    /// <summary>
    /// Classe de serviços para a entidade User
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// Realização da injeção do repositório na instância da classe.
        /// </summary>
        [Inject]
        public UserRepository Repository { get; set; }
        /// <summary>
        /// Obtém todos os Usuários.
        /// </summary>
        /// <returns>Uma lista de usuários</returns>
        public List<User> GetAll()
        {
            return Repository.FetchAll.ToList();
        }
        /// <summary>
        /// Obtém um usuário por id
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>Um objeto User</returns>
        public User GetById(int id)
        {
            return Repository.GetById(id);
        }
        /// <summary>
        /// Adiciona e salva um usuário na base.
        /// </summary>
        /// <param name="user">O objeto User a ser adicionado</param>
        public void Save(User user)
        {
            Repository.Add(user);
            Repository.Save();
        }
        /// <summary>
        /// Marca a alteração de estado e salva um usuário na base.
        /// </summary>
        /// <param name="user">O objeto user a ser alterado / editado</param>
        public void Update(User user)
        {
            Repository.Edit(user);
            Repository.Save();
        }
        /// <summary>
        /// Remove um usuário da base de dados e salva a base.
        /// </summary>
        /// <param name="user">O objeto a ser excluído</param>
        public void Delete(User user)
        {
            Repository.Delete(user);
            Repository.Save();
        }
        /// <summary>
        /// Obtém um usuário pela propriedade Login.
        /// </summary>
        /// <param name="login">O login do usuário.</param>
        /// <returns>Um objeto User</returns>
        public User GetByLogin(string login)
        {
            return Repository.FetchAll.FirstOrDefault(x => x.Login == login);
        }
    }
}
