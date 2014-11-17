using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MundiPagg.Blog.Service;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.WebUI.Models;
using System.Globalization;
using MundiPagg.Blog.WebUI.CustomController;
using Ninject;

namespace MundiPagg.Blog.WebUI.Controllers
{
    public class HomeController : BlogController //Controller
    {
        [Inject]
        public PostService _service { get; set; }

        private const int pageSize = 4;

        public ActionResult Index(int page = 1)
        {
            var posts = _service.GetPostsPaginated(page, pageSize);
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
                        PostedBy = "Postado por " + author + " em " + date
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

            return View(viewModel);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult SamplePost()
        {
            var posts = _service.GetAll();
            int postId = 0;
            Random rnd = new Random();
            Post post = null;

            while (post == null)
            {
                postId = rnd.Next(1, posts.Count());
                post = _service.GetById(postId);
            }

            return RedirectToAction("ViewPost", new { postId = post.PostId });
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult ViewPost(int postId = 0)
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

            return View("Post", model);
        }
    }
}
