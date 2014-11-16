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
            return Posts.FirstOrDefault(post => post.PostId == id);
        }

        public void Save(Post post)
        {
            if (post.PostId == 0)
            {
                var existingPost = context.Posts.FirstOrDefault(x => x.Title == post.Title);

                if (post.Title != existingPost.Title)
                {
                    context.Posts.Add(post);
                    context.SaveChanges();
                }
            }
        }

        public void Update(Post post)
        {
            var entity = context
                .Posts
                .FirstOrDefault(x => x.PostId == post.PostId);

            if (entity != null)
            {
                entity = post;
                context.SaveChanges();
            }
        }

        public void Delete(Post post)
        {
            if (post != null)
            {
                context.Posts.Remove(post);
                context.SaveChanges();
            }
        }
    }
}
