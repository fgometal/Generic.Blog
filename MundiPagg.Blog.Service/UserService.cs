﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ninject;

using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Repository.Interfaces;
using MundiPagg.Blog.Service.Interfaces;

namespace MundiPagg.Blog.Service
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        [Inject]
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Save(User user)
        {
            _repository.Save(user);
        }

        public void Delete(User user)
        {
            _repository.Delete(user);
        }
    }
}
