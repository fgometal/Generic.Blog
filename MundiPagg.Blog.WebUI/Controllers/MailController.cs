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

        //[HttpPost]
        //public JsonResult SendMail(MailModel model)
        [HttpPost]
        public ActionResult SendMail(MailModel model)
        {
            if (ModelState.IsValid)
            {
                var result = "";

                if (model != null)
                    result = "{ true }";
                else
                    result = "{ false }";
            }

            return View("Index");
           // return Json(result);

            //return Json(
            //    result,
            //    "",
            //    JsonRequestBehavior.AllowGet);
        }
    }
}
