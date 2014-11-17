using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MundiPagg.Blog.Domain.Entities;

namespace MundiPagg.Blog.WebUI.CustomSessionControl
{
    public static class SessionHelper
    {
        private const string _userSession = "usersSession";

        public static User User
        {
            get { return (User) HttpContext.Current.Session[_userSession];}
            set { HttpContext.Current.Session[_userSession] = value; }
        }

        public static bool HasUserLogged()
        {
            return User != null;
        }

        public static void CleanSession()
        {
            HttpContext.Current.Session.Abandon();
        }
    }
}
