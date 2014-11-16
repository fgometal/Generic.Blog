using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Service;
using MundiPagg.Blog.Service.Interfaces;
using MundiPagg.Blog.WebUI.Controllers;

namespace MundiPagg.Blog.UnitTests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private IUserService _service = new UserService();
        private List<User> users;
        private List<Post> posts;

        [TestInitialize]
        public void PreTestInitialize()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            AddUsers();
            AddPosts();
        }

        #region Controller Tests

        [TestMethod]
        public void Index()
        {
            // Arrange
            Mock<IPostService> mock = new Mock<IPostService>();
            mock.Setup(m => m.GetAll()).Returns(posts);
            //mock.Setup(m => m.GetAll()).Returns(posts.AsQueryable());

            HomeController controller = new HomeController(mock.Object);
            var pageSize = 3;

            // Act
            //IEnumerable<Post> result = (IEnumerable<Post>)controller.List(pageSize).Model;


            //ViewResult result = controller.Index() as ViewResult;

            // Assert
            //Post[] postArray = result.ToArray();
            //Assert.IsTrue(postArray.Length == 3);
            //Assert.AreEqual(postArray[0].Title, "");
            //Assert.AreEqual(postArray[1].Title, "");
            //Assert.AreEqual(postArray[2].Title, "");

            //Assert.AreEqual("Welcome to ASP.NET MVC!", result.ViewBag.Message);

            //Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void About()
        {
            Mock<IPostService> mock = new Mock<IPostService>();
            mock.Setup(m => m.GetAll()).Returns(posts);
            //mock.Setup(m => m.GetAll()).Returns(posts.AsQueryable());
            // Arrange
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        #endregion

        #region UserService Tests

        [TestMethod]
        public void GetUsers()
        {
            //Arrange
            Mock<IUserService> mock = new Mock<IUserService>();
            mock.Setup(m => m.GetAll()).Returns(users);

            //Act
            IEnumerable<User> expected = users;
            IEnumerable<User> actual = mock.Object.GetAll();

            //Assert 
            CollectionAssert.AreEqual(expected.ToList(), actual.ToList(), "Usuários obtidos com sucesso.");
        }

        [TestMethod]
        public void CanSaveUser()
        {
            //Arrange
            var user = new User
            {
                Login = "scrooge",
                Password = "#abc123",
                Email = "rich@disney.com",
                FirstName = "Scrooge",
                LastName = "McDuck",
                AboutMe = "$$$$$$$$$$$",
                IsAdmin = false,
                IsActive = true,
                DateRegistered = new DateTime(2014, 11, 14)
            };

            Mock<IUserService> mock = new Mock<IUserService>();
            mock.Setup(m => m.Save(user)).Verifiable("Usuário já existe");

            // Act
            mock.Object.Save(user);

            //Assert
            mock.Verify(m => m.Save(user));
        }

        [TestMethod]
        public void CannotSaveUser()
        {
            //Arrange
            var user = new User
            {
                Login = "mmouse",
                Password = "#abc123",
                Email = "mouse@disney.com",
                FirstName = "Mickey",
                LastName = "Mouse",
                AboutMe = "I´m a very badass mice!",
                IsAdmin = true,
                IsActive = true,
                DateRegistered = new DateTime(2014, 11, 14)
            };

            Mock<IUserService> mock = new Mock<IUserService>();
            mock.Setup(m => m.Save(user)).Verifiable("Usuário já existe");

            // Act
            mock.Object.Save(user);

            //Assert
            mock.Verify(m => m.Save(user));
        }

        [TestMethod]
        public void CanUpdateUser()
        {
            //Arrange
            Mock<IUserService> mock = new Mock<IUserService>();
            mock.Setup(m => m.GetById(2)).Returns(users.FirstOrDefault(x => x.UserId == 2));

            //Act
            var user = mock.Object.GetById(2);
            user.IsActive = false;
            mock.Object.Save(user);

            //Assert
            Assert.AreEqual(user, mock.Object.GetById(user.UserId), "Usuário alterado com sucesso");
        }

        [TestMethod]
        public void CanDeleteUser()
        {
            //Arrange
            var user = users.FirstOrDefault(x => x.UserId == 2);            
            Mock<IUserService> mock = new Mock<IUserService>();
            mock.Setup(m => m.Delete(user)).Verifiable("Não foi possível excluir usuário");

            // Act
            mock.Object.Delete(user);

            //Assert
            mock.Verify(m => m.Delete(user));
        }

        [TestMethod]
        public void CannotDeleteInvalidUser()
        {
            //Arrange
            var user = users.FirstOrDefault(x => x.UserId == 4);
            Mock<IUserService> mock = new Mock<IUserService>();
            mock.Setup(m => m.Delete(user)).Verifiable("Não foi possível excluir usuário");

            // Act
            mock.Object.Delete(user);

            //Assert
            mock.Verify(m => m.Delete(It.IsAny<User>()), Times.Never());
        }

        #endregion

        #region PostService Tests

        [TestMethod]
        public void GetPosts()
        {
        }

        [TestMethod]
        public void AddPost()
        {
        }

        [TestMethod]
        public void EditPost()
        {
        }

        [TestMethod]
        public void DeletePost()
        {
        }

        [TestMethod]
        public void AddCommentToPost()
        {
        }

        [TestMethod]
        public void EditCommentOnPost()
        {
        }

        [TestMethod]
        public void DeleteCommentFromPost()
        {
        }

        #endregion

        #region Test Initializers

        private void AddUsers()
        {
            users = new List<User> {
                new User {
                    UserId = 1,
                    Login = "mmouse", 
                    Password = "#abc123", 
                    Email = "mouse@disney.com", 
                    FirstName = "Mickey", 
                    LastName = "Mouse", 
                    AboutMe = "I´m a very badass mice!", 
                    IsAdmin = true,
                    IsActive = true,
                    DateRegistered = new DateTime(2014, 11, 14)
                },
                new User {
                    UserId = 2,
                    Login = "dduck", 
                    Password = "#abc123", 
                    Email = "duck@disney.com", 
                    FirstName = "Donald", 
                    LastName = "Duck", 
                    AboutMe = "Quaaaaack! Quaaaa-quaaaaackk!!!", 
                    IsAdmin = false,
                    IsActive = true,
                    DateRegistered = new DateTime(2014, 11, 12)
                },
                new User {
                    UserId = 3,
                    Login = "goofy", 
                    Password = "#abc123", 
                    Email = "goofy@disney.com", 
                    FirstName = "Goofy", 
                    LastName = "Duuuhh... Goofy?", 
                    AboutMe = "Yo hooo?!", 
                    IsAdmin = false,
                    IsActive = true,
                    DateRegistered = new DateTime(2014, 11, 12)
                }
            };
        }

        private void AddPosts()
        {
            posts = new List<Post>();
        }

        #endregion
        // Anotações... 
        //Assert.IsTrue(expected.SequenceEqual(actual));

        //mock.Setup(m => m.ProcessSomething("param")).Retrurns("Result based on param");

        //foreach (Product p in products) {
        //  mock.Verify(m => m.UpdateProduct(p), Times.Once());
        //}
    }
}
