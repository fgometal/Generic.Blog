using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Repository.Interfaces;
using MundiPagg.Blog.Repository.EFDbContext;

namespace MundiPagg.Blog.Repository
{
    public class UserRepository : IUserRepository
    {
        private BlogDBContext context = new BlogDBContext();

        public IQueryable<User> Users
        {
            get { return context.Users; }
        }
    }
}
