using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Infrastructure.Web.Mvc3;
using MundiPagg.Blog.Service;
using MundiPagg.Blog.Service.Interfaces;
using Ninject;

namespace MundiPagg.Blog.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                null,
                "Page/{page}",
                new { Controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                null,
                "Post/{postId}",
                new { Controller = "Home", action = "ViewPost" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            //Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");
            //AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            SetupDependencyInjection();
        }

        #region Dependency Injection

        /// <summary>
        /// 
        /// </summary>
        public void SetupDependencyInjection()
        {
            IKernel kernel = new StandardKernel();

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IPostService>().To<PostService>();
            kernel.Bind<IPostCommentaryService>().To<PostCommentaryService>();

            DependencyResolver.SetResolver(new NinjectDependecyResolver(kernel));
        }

        #endregion
    }
}