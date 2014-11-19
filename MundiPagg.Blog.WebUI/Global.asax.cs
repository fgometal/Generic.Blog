using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Infrastructure.Web.Mvc3;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Service;
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
              "Post/View/{postId}",
              new { Controller = "Post", action = "Index" }
            );

            routes.MapRoute(
               null,
               "Post/List/{page}",
               new { Controller = "Post", action = "List" }
           );

            routes.MapRoute(
               null,
               "Post/Edit/{postId}",
               new { Controller = "Post", action = "Edit" }
           );

            //  routes.MapRoute(
            //    null,
            //    "Post/Delete/{postId}",
            //    new { Controller = "Post", action = "Delete" }
            //);

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
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
            DependencyResolver.SetResolver(new NinjectDependecyResolver(kernel));
        }

        #endregion
    }
}