using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MundiPagg.Blog.Domain.Entities;

namespace MundiPagg.Blog.Repository.Interfaces
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }

        Post GetById(int id);
        void Save(Post post);
        void Update(Post post);
        void Delete(Post post);
    }
}
