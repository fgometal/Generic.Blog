using System.Web.Mvc;

namespace MundiPagg.Blog.WebUI.Controllers
{
    /// <summary>
    /// Classe para a controller Ajax.
    /// </summary>
    public class AjaxController : Controller
    {
        #region Actions

        /// <summary>
        /// Processa a requisição de exibição da View Index
        /// </summary>
        /// <returns>A View Index</returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Processa uma requisição ajax recebendo dados vindos de um objeto json.
        /// </summary>
        /// <param name="paramone">Parametro de número 1</param>
        /// <param name="paramtwo">Parametro de número 2</param>
        /// <returns>Um objeto json com as informações processadas.</returns>
        public JsonResult JsonExample(string paramone, string paramtwo)
        {
            var paramOne = "Não preenchido";
            var paramTwo = "Não preenchido";

            if (paramone != string.Empty)
                paramOne = paramone;

            if (paramtwo != string.Empty)
                paramTwo = paramtwo;

            var data = new
            {
                ParamOne = paramOne,
                ParamTwo = paramTwo,
                Success = true
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
