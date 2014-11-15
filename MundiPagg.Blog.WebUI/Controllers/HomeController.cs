using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MundiPagg.Blog.Service.Interfaces;
using MundiPagg.Blog.Service;

namespace MundiPagg.Blog.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _service;
        private const int pageSize = 3;

        public HomeController(IPostService postService)
        {
            _service = postService;
        }

        public void List(int page = 1)
        {
            _service.GetPostPaginated(page, pageSize);
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
