using System.Web.Mvc;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.WebUI.CustomSessionControl;

namespace MundiPagg.Blog.WebUI.CustomController
{
    public abstract class BlogController : Controller
    {
        public User CurrentUser
        {
            get { return SessionHelper.User; }
        }
    }
}
