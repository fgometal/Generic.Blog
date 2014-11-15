using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Repository.Interfaces;
using MundiPagg.Blog.Repository.DatabaseContext;

namespace MundiPagg.Blog.Repository
{
    public class PostRepository : IPostRepository
    {
        private BlogDBContext context = new BlogDBContext();

        public IQueryable<Post> Posts
        {
            get { return context.Posts; }
        }

        public Post GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Post post)
        {
            throw new NotImplementedException();
        }

        public void Delete(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
