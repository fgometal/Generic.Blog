using System.Collections.Generic;
using System.Linq;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Repository.Interfaces;
using MundiPagg.Blog.Service.Interfaces;
using MundiPagg.Blog.Repository;

namespace MundiPagg.Blog.Service
{
    public class PostCommentaryService : IPostCommentaryService
    {
        private IPostCommentaryRepository _repository = new PostCommentaryRepository();

        public List<PostCommentary> GetAll()
        {
            return _repository.Commentaries.ToList();
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
