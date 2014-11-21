using System.Web;
using MundiPagg.Blog.Domain.Entities;

namespace MundiPagg.Blog.WebUI.CustomSessionControl
{
    /// <summary>
    /// Classe para controle de obtenção de usuário na sessão.
    /// </summary>
    public static class SessionHelper
    {
        /// <summary>
        /// String do nome da sessão do usuário.
        /// </summary>
        private const string _userSession = "usersSession";
        /// <summary>
        /// Propriedade estática com o usuário obtdo da sessão.
        /// </summary>
        public static User User
        {
            get { return (User) HttpContext.Current.Session[_userSession];}
            set { HttpContext.Current.Session[_userSession] = value; }
        }
        /// <summary>
        /// Verifica se existe usuário logado
        /// </summary>
        /// <returns></returns>
        public static bool HasUserLogged()
        {
            return User != null;
        }
        /// <summary>
        /// Limpa o estado da sessão de usuário.
        /// </summary>
        public static void CleanSession()
        {
            HttpContext.Current.Session.Abandon();
        }
    }
}
