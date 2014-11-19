using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Service;
using MundiPagg.Blog.WebUI.CustomController;
using MundiPagg.Blog.WebUI.Models;
using Ninject;

namespace MundiPagg.Blog.WebUI.Controllers
{
    public class PostController : BlogController
    {
        [Inject]
        public PostService _service { get; set; }

        //
        // GET: /Post/

        public ActionResult Index(int postId = 0)
        {
            PostModel model = null;
            var post = _service.GetById(postId);

            if (post != null)
            {
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

        [Authorize]
        public ActionResult Edit(int postId = 0)
        {
            var model = new EditPostModel();

            if (postId > 0)
            {
                var post = _service.GetById(postId);

                model.PostId = post.PostId;
                model.Title = post.Title;
                model.Summary = post.Summary;
                model.Content = post.PostContent;
                model.Tags = post.Tags;
            }

            return View(model);
        }

        [Authorize]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(EditPostModel model)
        {
            if (ModelState.IsValid)
            {
                var updateMessage = "";

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

                    _service.Save(post);
                    TempData["Notification"] = "Seu post foi adicionado com sucesso.";
                }
                else
                {
                    var post = _service.GetById(model.PostId);

                    post.Title = model.Title;
                    post.Summary = model.Summary;
                    post.PostContent = model.Content;
                    post.Tags = model.Tags;
                    post.User = CurrentUser;
                    post.Commentaries = new Collection<PostCommentary>();
                    post.EditDate = DateTime.Now;
                    post.IsActive = true;

                    _service.Update(post);
                    TempData["Notification"] = "Seu post foi alterado com sucesso.";
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [Authorize]
        public ActionResult List(int page = 1)
        {
            var pageSize = 4;
            var posts = _service.GetPostsByUserId(page, pageSize, CurrentUser.UserId);
            var postPreviews = new List<PostModel>();

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

            var viewModel = new IndexViewModel
            {
                Posts = postPreviews,
                PagingInfo = new PagingInfoModel
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = _service.GetAll().Count()
                }
            };

            return View("List", viewModel);
        }

        [Authorize]
        public ActionResult Delete(int postId)
        {
            var post = _service.GetById(postId);

            if (post != null)
            {
                _service.Delete(post);
                TempData["Notification"] = "Seu post foi excluído com sucesso.";
            }

            return RedirectToAction("List");
        }
    }
}
