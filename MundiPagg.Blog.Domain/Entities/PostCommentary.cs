using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MundiPagg.Blog.Domain.Entities
{
    [Table("PostCommentary")]
    public class PostCommentary
    {
        [Key]
        public int PostCommentaryId { get; set; }
        public string Commentary { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsActive { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
