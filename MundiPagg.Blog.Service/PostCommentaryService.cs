using System.Collections.Generic;
using System.Linq;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Repository.Interfaces;
using MundiPagg.Blog.Repository;
using Ninject;

namespace MundiPagg.Blog.Service
{
    public class PostCommentaryService
    {
        [Inject]
        public PostCommentaryRepository _repository { get; set; }

        public List<PostCommentary> GetAll()
        {
            return _repository.FetchAll.ToList();
        }

        public PostCommentary GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Save(PostCommentary commentary)
        {
            _repository.Add(commentary);
            _repository.Save();
        }

        public void Update(PostCommentary commentary)
        {
            _repository.Edit(commentary);
            _repository.Save();
        }

        public void Delete(PostCommentary commentary)
        {
            _repository.Delete(commentary);
            _repository.Save();
        }
    }
}
