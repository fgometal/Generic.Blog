using System;
using System.Text;
using System.Web.Mvc;
using MundiPagg.Blog.WebUI.Models;

namespace MundiPagg.Blog.WebUI.HtmlHelpers
{
    /// <summary>
    /// Classe de controle de paginação.
    /// </summary>
    public static class PagingHelpers
    {
        /// <summary>
        /// Extensão para a classe Html Helper para exibição e controle de links de paginação.
        /// </summary>
        /// <param name="html">Instância para extensão do HtmlHelper</param>
        /// <param name="pagingInfo">Data model com as informações de paginação</param>
        /// <param name="pageUrl">Método com a url da página.</param>
        /// <returns>Um html com o link da página.</returns>
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfoModel pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");

                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();

                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");

                result.Append(tag.ToString());
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}
