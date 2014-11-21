using System;

namespace MundiPagg.Blog.WebUI.Models
{
    /// <summary>
    /// Classe para a data model de controle de paginação
    /// </summary>
    public class PagingInfoModel
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        /// <summary>
        /// Propriedade para obter o total de páginas a serem renderizadas.
        /// </summary>
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
            }
        }
    }
}
