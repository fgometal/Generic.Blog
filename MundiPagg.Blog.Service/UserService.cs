using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Repository.Interfaces;
using MundiPagg.Blog.Service.Interfaces;
using Ninject;

namespace MundiPagg.Blog.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        [Inject]
        public UserService(IUserRepository repo)
        {
            this._repo = repo;
        }

        public void Test(User user)
        {
            _repo.Save(user);
        }
    }
}
