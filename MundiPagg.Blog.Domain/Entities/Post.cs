using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MundiPagg.Blog.Domain.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string PostContent { get; set; }
        public string Tags { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime EditDate { get; set; }
        public bool IsActive { get; set; }
        public User User { get; set; }
        public List<PostCommentary> Commentaries { get; set; }
    }
}
