using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MundiPagg.Blog.Domain.Entities;

namespace MundiPagg.Blog.WebUI.Models
{
    public class PostModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public string PostedBy { get; set; }
    }
}
