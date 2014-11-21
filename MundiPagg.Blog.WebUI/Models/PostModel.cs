using MundiPagg.Blog.Domain.Entities;

namespace MundiPagg.Blog.WebUI.Models
{
    /// <summary>
    /// Classe para a data model de Posts.
    /// </summary>
    public class PostModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public string PostedBy { get; set; }
        public string PublishDate { get; set; }
        public string EditDate { get; set; }
    }
}
