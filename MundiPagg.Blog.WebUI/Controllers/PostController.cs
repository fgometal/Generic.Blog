using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Service;
using MundiPagg.Blog.WebUI.CustomController;
using MundiPagg.Blog.WebUI.Models;
using Ninject;

namespace MundiPagg.Blog.WebUI.Controllers
{
    /// <summary>
    /// Classe da controller de Posts
    /// </summary>
    public class PostController : BlogController
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
        /// Processa a requisição de exibição de Post.
        /// </summary>
        /// <param name="postId">Id do post a ser buscado.</param>
        /// <returns>Um data model com as informações do post ser exibido pela view.</returns>
        public ActionResult Index(int postId = 0)
        {
            PostModel model = null;
            var post = PostService.GetById(postId);

            if (post != null)
            {
                // Realiza as concatenações de strings para compor o nome e a data de publicação.
                var author = post.User.FirstName + " " + post.User.LastName;
                var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(post.PublishDate.Month);
                var date = post.PublishDate.Day + " de " + month + " de " + post.PublishDate.Year;

                model = new PostModel();
                model.PostId = post.PostId;
                model.Title = post.Title;
                model.Summary = post.Summary;
                model.Content = post.PostContent;
                model.User = post.User;
                model.PostedBy = "Postado por " + author + " em " + date;
            }

            return View(model);
        }
        /// <summary>
        /// Processa a requisição de exibição da View de edição de Posts
        /// trazendo um data model com o post indicado, ou caso a is seja == 0, 
        /// é exibida a View pronta para se criar um novo post.
        /// </summary>
        /// <param name="postId">Id do post a ser editado</param>
        /// <returns>Um data model com as informações do post a ser editado.</returns>
        [Authorize]
        public ActionResult Edit(int postId = 0)
        {
            var model = new EditPostModel();

            if (postId > 0)
            {
                var post = PostService.GetById(postId);

                model.PostId = post.PostId;
                model.Title = post.Title;
                model.Summary = post.Summary;
                model.Content = post.PostContent;
                model.Tags = post.Tags;
            }

            return View(model);
        }
        /// <summary>
        /// Processa a requisição de edição ou de salvar um novo Post.
        /// </summary>
        /// <param name="model">Id do post a ser editado e alterado</param>
        /// <returns>Um redirecionamento para a View Index da HomeController, ou
        /// a View de edição caso ocorram erros.</returns>
        [Authorize]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(EditPostModel model)
        {
            if (ModelState.IsValid)
            {
                // Mesagem de notificação.
                var notificationMessage = "";

                // Salva  post
                if (model.PostId == 0)
                {
                    var post = new Post
                    {
                        Title = model.Title,
                        Summary = model.Summary,
                        PostContent = model.Content,
                        Tags = model.Tags,
                        User = CurrentUser,
                        Commentaries = new Collection<PostCommentary>(),
                        EditDate = DateTime.Now,
                        PublishDate = DateTime.Now,
                        IsActive = true,
                    };

                    PostService.Save(post);
                    TempData["Notification"] = "Seu post foi adicionado com sucesso.";
                }
                else
                {
                    // Edita o post.
                    var post = PostService.GetById(model.PostId);

                    post.Title = model.Title;
                    post.Summary = model.Summary;
                    post.PostContent = model.Content;
                    post.Tags = model.Tags;
                    post.User = CurrentUser;
                    post.Commentaries = new Collection<PostCommentary>();
                    post.EditDate = DateTime.Now;
                    post.IsActive = true;

                    PostService.Update(post);
                    TempData["Notification"] = "Seu post foi alterado com sucesso.";
                }
                // Redirect para a Home.
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
        /// <summary>
        /// Processa a requisição de exibição da lista de posts criados
        /// pelo usuário logado.
        /// </summary>
        /// <param name="page">Página da lista de exibição.</param>
        /// <returns>A View List na página solicitada.</returns>
        [Authorize]
        public ActionResult List(int page = 1)
        {
            var posts = PostService.GetPostsByUserId(page, pageSize, CurrentUser.UserId);
            var postPreviews = new List<PostModel>();

            // Obtém a lista de posts e preenche a lista de PostModel.
            if (posts.Count > 0)
            {
                foreach (var item in posts)
                {
                    var author = item.User.FirstName + " " + item.User.LastName;
                    var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.PublishDate.Month);
                    var date = item.PublishDate.Day + " de " + month + " de " + item.PublishDate.Year;

                    postPreviews.Add(new PostModel
                    {
                        PostId = item.PostId,
                        Title = item.Title,
                        Summary = item.Summary,
                        PostedBy = "Postado por " + author + " em " + date,
                        PublishDate = item.PublishDate.ToShortDateString(),
                        EditDate = item.EditDate.ToShortDateString()
                    });
                }
            }

            // Preenche as informações da data model para exibição
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

            return View("List", viewModel);
        }
        /// <summary>
        /// Processa a requisição de Exclusão de um post.
        /// </summary>
        /// <param name="postId">Id do post a ser excluído.</param>
        /// <returns>Um redirecionamento para a View List</returns>
        [Authorize]
        public ActionResult Delete(int postId)
        {
            var post = PostService.GetById(postId);

            if (post != null)
            {
                PostService.Delete(post);
                TempData["Notification"] = "Seu post foi excluído com sucesso.";
            }

            return RedirectToAction("List");
        }

        #endregion
    }
}
