using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MundiPagg.Blog.Domain.Entities;

namespace MundiPagg.Blog.Repository.Interfaces
{
    public interface IPostCommentaryRepository
    {
        IQueryable<PostCommentary> Commentaries { get; }

        PostCommentary GetById(int id);
        void Save(PostCommentary commentary);
        void Delete(PostCommentary commentary);
    }
}
