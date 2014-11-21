using System.Collections.Generic;

namespace MundiPagg.Blog.WebUI.Models
{
    /// <summary>
    /// Classe de data model para a Home
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Lista dos posts da página
        /// </summary>
        public IEnumerable<PostModel> Posts { get; set; }
        /// <summary>
        /// Informações para o controle de paginação
        /// </summary>
        public PagingInfoModel PagingInfo { get; set; }
    }
}
