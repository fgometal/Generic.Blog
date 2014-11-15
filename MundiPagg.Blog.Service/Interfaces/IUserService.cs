using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MundiPagg.Blog.Domain.Entities;

namespace MundiPagg.Blog.Service.Interfaces
{
    public interface IUserService
    {
        List<User> GetAll();
        User GetById(int id);
        void Save(User user);
        void Update(User user);
        void Delete(User user);
    }
}
