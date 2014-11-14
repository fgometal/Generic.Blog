using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MundiPagg.Blog.Domain.Entities
{
    public class PostCommentary
    {
        public int PostCommentaryId { get; set; }
        public string Commentary { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsActive { get; set; }
        public User User { get; set; }
    }
}
