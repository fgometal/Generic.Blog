using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace MundiPagg.Blog.Domain.Entities
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string PostContent { get; set; }
        public string Tags { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime EditDate { get; set; }
        public bool IsActive { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual Collection<PostCommentary> Commentaries { get; set; }
    }
}
