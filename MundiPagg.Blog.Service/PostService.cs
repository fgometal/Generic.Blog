using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ninject;

using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Repository.Interfaces;
using MundiPagg.Blog.Service.Interfaces;

namespace MundiPagg.Blog.Service
{
    public class PostService : IPostService
    {
        private IPostRepository _repository;

        [Inject]
        public PostService(IPostRepository repository)
        {
            _repository = repository;
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
    }
}
