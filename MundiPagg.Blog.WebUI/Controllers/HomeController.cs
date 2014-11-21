using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using MundiPagg.Blog.Service;
using MundiPagg.Blog.WebUI.CustomController;
using MundiPagg.Blog.WebUI.Models;
using Ninject;

namespace MundiPagg.Blog.WebUI.Controllers
{
    /// <summary>
    /// Classe para a controller da Home.
    /// </summary>
    public class HomeController : BlogController
    {
        /// <summary>
        /// Realização da injeção do serviço na instância da classe.
        /// </summary>
        [Inject]
        public PostService PostService { get; set; }

        // Comprimento da lista por página.
        private const int pageSize = 4;

        #region Actions

        /// <summary>
        /// Processa a requisição de exibição da Home. Aqui são apresentados os posts
        /// de todos os usuários listados por paginação.
        /// </summary>
        /// <param name="page">Página da exibição da lista atual de posts</param>
        /// <returns>Um data model com a exibição dos posts e informações de pagonaçaõ</returns>
        public ActionResult Index(int page = 1)
        {
            // Posts são obtidos por página.
            var posts = PostService.GetPostsPaginated(page, pageSize);
            var postPreviews = new List<PostModel>();

            if (posts.Count > 0)
            {
                // Passam-se os posts para a lista do data model de exibição.
                foreach (var item in posts)
                {
                    // Realiza as concatenações de strings para compor o nome e a data de publicação.
                    var author = item.User.FirstName + " " + item.User.LastName;
                    var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.PublishDate.Month);
                    var date = item.PublishDate.Day + " de " + month + " de " + item.PublishDate.Year;

                    postPreviews.Add(new PostModel
                    {
                        PostId = item.PostId,
                        Title = item.Title,
                        Summary = item.Summary,
                        PostedBy = "Postado por " + author + " em " + date,
                        PublishDate = item.PublishDate.ToShortDateString()
                    });
                }
            }

            // Cria-se o data model de exibição dos posts e informações de paginação.
            var viewModel = new IndexViewModel
            {
                Posts = postPreviews,
                PagingInfo = new PagingInfoModel
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = PostService.GetAll().Count()
                }
            };

            return View(viewModel);
        }
        /// <summary>
        /// Processa a requisição de exibição da View About (Sobre)
        /// </summary>
        /// <returns>A view About</returns>
        public ActionResult About()
        {
            return View();
        }
        /// <summary>
        /// Processa a requisição de exibição da View Contact (Contato)
        /// </summary>
        /// <returns>A view Contact</returns>
        public ActionResult Contact()
        {
            return View();
        }
        /// <summary>
        /// Processa a requisição de enviodas informações do formulário de contato.
        /// </summary>
        /// <param name="model">Model com as informações do form.</param>
        /// <returns>A view Contact com uma mensagem de notificação.</returns>
        [HttpPost]
        public ActionResult SendMail(MailModel model)
        {
            if (ModelState.IsValid)
            {
                // Passa para o TempData a mensagem de cofirmação de envio de mensagem.
                TempData["Notification"] = "Obrigado " + model.Name + ". Em breve retornamos seu contato. :)";
            }

            return View("Contact", model);
        }

        #endregion
    }
}
