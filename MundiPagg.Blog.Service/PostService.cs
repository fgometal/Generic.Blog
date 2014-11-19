using System.Collections.Generic;
using System.Linq;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Repository.Interfaces;
using MundiPagg.Blog.Repository;
using Ninject;

namespace MundiPagg.Blog.Service
{
    public class PostService
    {
        [Inject]
        public PostRepository _repository { get; set; }

        public List<Post> GetAll()
        {
            return _repository.FetchAll.ToList();
        }

        public Post GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Save(Post post)
        {
            _repository.Add(post);
            _repository.Save();
        }

        public void Update(Post post)
        {
            _repository.Edit(post);
            _repository.Save();
        }

        public void Delete(Post post)
        {
            _repository.Delete(post);
            _repository.Save();
        }

        public List<Post> GetPostsPaginated(int page, int pageSize)
        {
            return GetAll()
                .OrderByDescending(post => post.PublishDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public List<Post> GetPostsByUserId(int page, int pageSize, int userId)
        {
            return GetPostsPaginated(page, pageSize)
                .Where(post => post.User.UserId == userId)
                .ToList();
        }
    }
}
