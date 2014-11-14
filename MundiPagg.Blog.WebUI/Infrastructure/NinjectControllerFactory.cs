using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

using Moq;
using Ninject;

using MundiPagg.Blog.Repository.Interfaces;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Repository;
using MundiPagg.Blog.Service;
using MundiPagg.Blog.Service.Interfaces;

namespace MundiPagg.Blog.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        public class NinjectControllerFactory : DefaultControllerFactory
        {
            #region Ninject Kernel Initialization

            private IKernel ninjectKernel;

            public NinjectControllerFactory()
            {
                ninjectKernel = new StandardKernel();
                AddBindings();
            }

            protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
            {
                return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
            }

            private void AddBindings()
            {
                // put additional bindings here
                MockEmAll();
                //ninjectKernel.Bind<IUserRepository>().To<UserRepository>();
                //ninjectKernel.Bind<IPostRepository>().To<PostRepository>();
                //ninjectKernel.Bind<IPostCommentaryRepository>().To<PostCommentaryRepository>();
                //ninjectKernel.Bind<IUserService>().To<UserService>();   
                //ninjectKernel.Bind<IPostService>().To<PostService>();
                //ninjectKernel.Bind<IPostCommentaryService>().To<PostCommentaryService>();
            }

            #endregion

            #region DoMocking

            /// <summary>
            /// 
            /// </summary>
            private void MockEmAll()
            {
                MockUsers();
            }
            /// <summary>
            /// 
            /// </summary>
            private void MockUsers()
            {
                Mock<IUserRepository> mock = new Mock<IUserRepository>();

                mock.Setup(m => m.Users).Returns(new List<User>
                {
                    new User { 
                        Login = "mmouse", 
                        Password = "#abc123", 
                        Email = "mouse@disney.com", 
                        FirstName = "Mickey", 
                        LastName = "Mouse", 
                        AboutMe = "I´m a very badass mice!", 
                        DateRegistered = new DateTime(2014, 11, 14),
                        IsAdmin = true
                    }
                }.AsQueryable());

                ninjectKernel.Bind<IUserRepository>().ToConstant(mock.Object);
            }

            #endregion
        }
    }
}

