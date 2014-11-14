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
    public class PostCommentaryService : IPostCommentaryService
    {
        private IPostCommentaryRepository _repository;

        [Inject]
        public PostCommentaryService(IPostCommentaryRepository repository)
        {
            _repository = repository;
        }

        public PostCommentary GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Save(PostCommentary commentary)
        {
            _repository.Save(commentary);
        }

        public void Delete(PostCommentary commentary)
        {
            _repository.Delete(commentary);
        }
    }
}
