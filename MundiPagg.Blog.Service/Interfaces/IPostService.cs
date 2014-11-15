using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MundiPagg.Blog.Domain.Entities;

namespace MundiPagg.Blog.Service.Interfaces
{
    public interface IPostService
    {
        List<Post> GetAll();
        Post GetById(int id);
        void Save(Post post);
        void Delete(Post post);
        List<Post> GetPostPaginated(int page, int pageSize);
    }
}
