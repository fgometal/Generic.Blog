using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Repository.Interfaces;
using MundiPagg.Blog.Repository.DatabaseContext;

namespace MundiPagg.Blog.Repository
{
    public class UserRepository : IUserRepository
    {
        private BlogDBContext context = new BlogDBContext();

        public IQueryable<User> Users
        {
            get { return context.Users; }
        }

        public User GetById(int id)
        {
            return Users.FirstOrDefault(user => user.UserId == id);
        }

        public void Save(User user)
        {
            if (user.UserId == 0)
            {
                var existingUser = context.Users.FirstOrDefault(x => x.Login == user.Login);

                if (user.Login != existingUser.Login)
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
        }

        public void Update(User user)
        {
            var entity = context
                .Users
                .FirstOrDefault(x => x.UserId == user.UserId);

            if (entity != null)
            {
                entity = user;
                context.SaveChanges();
            }
        }

        public void Delete(User user)
        {
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }
    }
}
