using System.Collections.Generic;
using System.Linq;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Repository.Interfaces;
using MundiPagg.Blog.Service.Interfaces;
using MundiPagg.Blog.Repository;

namespace MundiPagg.Blog.Service
{
    public class UserService : IUserService
    {
        private IUserRepository _repository = new UserRepository();

        public List<User> GetAll()
        {
            return _repository.Users.ToList();
        }
        public User GetById(int id)
        {
            return _repository.GetById(id);
        }
        public void Save(User user)
        {
            _repository.Save(user);
        }
        public void Update(User user)
        {
            _repository.Update(user);
        }
        public void Delete(User user)
        {
            _repository.Delete(user);
        }
    }
}
