using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Blog.WebUI.Models
{
    public class IndexViewModel
    {
        public IEnumerable<PostModel> Posts { get; set; }
        public PagingInfoModel PagingInfo { get; set; }
    }
}
