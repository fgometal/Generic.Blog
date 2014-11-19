using System.Collections.Generic;
using System.Linq;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Repository.Interfaces;
using MundiPagg.Blog.Repository;
using Ninject;

namespace MundiPagg.Blog.Service
{
    public class UserService
    {
        [Inject]
        public UserRepository _repository { get; set; }

        public List<User> GetAll()
        {
            return _repository.FetchAll.ToList();
        }

        public User GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Save(User user)
        {
            _repository.Add(user);
            _repository.Save();
        }

        public void Update(User user)
        {
            _repository.Edit(user);
            _repository.Save();
        }

        public void Delete(User user)
        {
            _repository.Delete(user);
            _repository.Save();
        }

        public User GetByLogin(string login)
        {
            return _repository.FetchAll.FirstOrDefault(x => x.Login == login);
        }
    }
}
