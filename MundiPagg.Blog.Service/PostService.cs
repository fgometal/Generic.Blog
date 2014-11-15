using System.Collections.Generic;
using System.Linq;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Repository.Interfaces;
using MundiPagg.Blog.Service.Interfaces;
using MundiPagg.Blog.Repository;

namespace MundiPagg.Blog.Service
{
    public class PostService : IPostService
    {
        private IPostRepository _repository = new PostRepository();

        public List<Post> GetAll()
        {
            return _repository.Posts.ToList();
        }

        public Post GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Save(Post post)
        {
            _repository.Save(post);
        }

        public void Delete(Post post)
        {
            _repository.Delete(post);
        }

        public List<Post> GetPostPaginated(int page, int pageSize)
        {
            return GetAll()
                .OrderByDescending(post => post.PublishDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
