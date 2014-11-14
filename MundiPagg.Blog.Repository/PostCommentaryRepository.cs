using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Repository.Interfaces;
using MundiPagg.Blog.Repository.EFDbContext;

namespace MundiPagg.Blog.Repository
{
    public class PostCommentaryRepository : IPostCommentaryRepository
    {
        private BlogDBContext context = new BlogDBContext();

        public IQueryable<PostCommentary> Commentaries
        {
            get { return context.Commentaries; }
        }

        public PostCommentary GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(PostCommentary commentary)
        {
            throw new NotImplementedException();
        }

        public void Delete(PostCommentary commentary)
        {
            throw new NotImplementedException();
        }
    }
}
