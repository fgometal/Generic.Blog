using System.Web.Mvc;
using System.Web.Routing;
using Infrastructure.Web.Mvc3;
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

            // Home, exibição dos posts paginada
            routes.MapRoute(
               null,
               "Page/{page}",
               new { Controller = "Home", action = "Index" }
            );
            // Visualizar post
            routes.MapRoute(
              null,
              "Post/View/{postId}",
              new { Controller = "Post", action = "Index" }
            );
            // Listar posts do usuário paginado
            routes.MapRoute(
               null,
               "Post/List/{page}",
               new { Controller = "Post", action = "List" }
           );
            // Edição / criação de posts
            routes.MapRoute(
               null,
               "Post/Edit/{postId}",
               new { Controller = "Post", action = "Edit" }
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
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            SetupDependencyInjection();
        }

        #region Dependency Injection

        /// <summary>
        /// Inicialização da classe de controle da injeção de dependência.
        /// Passa-se uma instância do kernel do Ninject para a classe de controle
        /// e em seguida uma nova instância dessa classe para o método 
        /// DependencyResolver.SetResolver
        /// </summary>
        public void SetupDependencyInjection()
        {
            IKernel kernel = new StandardKernel();
            DependencyResolver.SetResolver(new NinjectDependecyResolver(kernel));
        }

        #endregion
    }
}