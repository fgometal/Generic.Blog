using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MundiPagg.Blog.WebUI.Models;

namespace MundiPagg.Blog.WebUI.Controllers
{
    public class MailController : Controller
    {
        //
        // GET: /Mail/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SendMail(MailModel model)
        {
            if (model != null)
            { 
            }

            return Json(
                new { success = true },
                "",
                JsonRequestBehavior.AllowGet);
        }
    }
}
