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
    public class HomeController : BlogController
    {
        [Inject]
        public PostService _service { get; set; }

        private const int pageSize = 4;

        public ActionResult Index(int page = 1, bool updated = false)
        {
            ViewBag.Notification = updated;

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

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMail(MailModel model)
        {
            ViewBag.Success = false;

            if (ModelState.IsValid)
            {
                ViewBag.Message = "Obrigado " + model.Name + ". Em breve retornamos seu contato. :)";
                ViewBag.Success = true;
            }

            return View("Contact", model);
        }
    }
}
