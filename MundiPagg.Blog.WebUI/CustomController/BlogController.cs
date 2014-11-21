using System.Web.Mvc;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.WebUI.CustomSessionControl;

namespace MundiPagg.Blog.WebUI.CustomController
{
    /// <summary>
    /// Classe de controller customizada herdando de Mvc.Controller.
    /// </summary>
    public abstract class BlogController : Controller
    {
        /// <summary>
        /// Armazena um objeto User
        /// </summary>
        private static User _user = null;
        /// <summary>
        /// Obtém a instância de usuário atual logado.
        /// </summary>
        public User CurrentUser
        {
            get 
            {
                if (_user == null)
                    return SessionHelper.User;
                else
                    return _user;
            }
            // Utilizar esse Set somente no projeto de testes
            set { _user = value; } 
        }
    }
}
