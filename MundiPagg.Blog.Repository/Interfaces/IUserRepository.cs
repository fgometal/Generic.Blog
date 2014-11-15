using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MundiPagg.Blog.Domain.Entities;

namespace MundiPagg.Blog.Repository.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }

        User GetById(int id);
        void Save(User user);
        void Update(User user);
        void Delete(User user);
    }
}
