using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.WebUI.Controllers;
using MundiPagg.Blog.WebUI.Models;
using MundiPagg.Blog.WebUI.UnitTests.DatabaseContext;

namespace MundiPagg.Blog.UnitTests.Controllers
{
    /// <summary>
    /// Classe de testes para as actions da HomeController.
    /// </summary>
    [TestClass]
    public class HomeControllerTest
    {
        #region Tests Initialization

        /// <summary>
        /// Inicialização que corre antes de cada teste.
        /// </summary>
        [TestInitialize]
        public void PreTestInitialize()
        {
            DatabaseSetup.DatabaseStartUp();
        }

        #endregion

        #region Home Controller Tests

        /// <summary>
        /// Teste para a Index. Verifica se retorna os posts corretamente.
        /// </summary>
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();
            var page = 1;
            controller.PostService = DatabaseSetup.PostService;

            // Act
            var result = controller.Index(page) as ViewResult;
            var model = (IndexViewModel)result.ViewData.Model;

            //Assert
            PostModel[] postArray = model.Posts.ToArray();
            Assert.IsTrue(postArray.Length == 4, "Não foi possível obter resultados.");
            Assert.AreEqual(postArray[0].Title, "Post 1", "Os objetos comparados não correspondem.");
            Assert.AreEqual(postArray[1].Title, "Post 5", "Os objetos comparados não correspondem.");
            Assert.AreEqual(postArray[2].Title, "Post 4", "Os objetos comparados não correspondem.");
            Assert.AreEqual(postArray[3].Title, "Post 3", "Os objetos comparados não correspondem.");
        }
        /// <summary>
        /// Teste para About. Verifica se não retorna nulo.
        /// </summary>
        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// Teste para Contact. Verifica se não retorna nulo.
        /// </summary>
        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// Teste para SendMail. Verifica se retorna a mensagem esperada
        /// no TempData.
        /// </summary>
        [TestMethod]
        public void SendMail()
        {
            HomeController controller = new HomeController();

            var model = new MailModel
            {
                Name = "Name Test",
                Email = "test@test.com",
                Phone = "(21)1234-5678",
                Message = "Message of the mail"
            };

            var expected = "Obrigado " + model.Name + ". Em breve retornamos seu contato. :)";
            var result = controller.SendMail(model) as ViewResult;
            var tempData = controller.TempData["Notification"].ToString();

            Assert.AreEqual(expected, tempData);
        }
        /// <summary>
        /// Verifica se a action Index consegue paginar corretamente.
        /// </summary>
        [TestMethod]
        public void CanPaginatePosts()
        {
            //Arrange
            var controller = new HomeController();
            controller.PostService = DatabaseSetup.PostService;

            //Action 
            var result = controller.Index() as ViewResult;
            var model = (IndexViewModel)result.ViewData.Model;

            //Assert
            PostModel[] postArray = model.Posts.ToArray();
            Assert.IsTrue(postArray.Length == 4, "Não foi possível obter resultados.");
            Assert.AreEqual(postArray[0].Title, "Post 1", "Os objetos comparados não correspondem.");
            Assert.AreEqual(postArray[1].Title, "Post 5", "Os objetos comparados não correspondem.");
            Assert.AreEqual(postArray[2].Title, "Post 4", "Os objetos comparados não correspondem.");
            Assert.AreEqual(postArray[3].Title, "Post 3", "Os objetos comparados não correspondem.");
        }

        #endregion

        #region UserService Tests

        /// <summary>
        /// Verifica se o método GetAll() retorna resultados.
        /// </summary>
        [TestMethod]
        public void GetUsers()
        {
            //Arrange
            //Act
            IEnumerable<User> result = DatabaseSetup.UserService.GetAll();

            //Assert
            Assert.IsTrue(result.Count() > 0, "Nenhum resultado obtido.");
        }
        /// <summary>
        /// Verifica se GetByUserId retorna um usuário.
        /// </summary>
        [TestMethod]
        public void GetUserById()
        {
            //Arrange
            int userId = DatabaseSetup.UserService.GetAll().First().UserId;

            // Act
            var result = DatabaseSetup.UserService.GetById(userId);

            //Assert
            Assert.IsNotNull(result, "Não foi possível obter usuário.");
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetUserByLogin()
        {
            // Arrange
            // Act
            var result = DatabaseSetup.UserService.GetByLogin("userone");

            // Assert
            Assert.IsNotNull(result, "Não foi possível obter o usuário.");
        }
        /// <summary>
        /// Testa o método de Save.
        /// </summary>
        [TestMethod]
        public void SaveUser()
        {
            //Arrange
            var user = new User
            {
                Login = "userfive",
                Email = "user5@disney.com",
                FirstName = "User",
                LastName = "Five",
                AboutMe = "I´m a dummy user",
                IsAdmin = false,
                IsActive = true,
                DateRegistered = DateTime.Now
            };

            // Act
            DatabaseSetup.UserService.Save(user);
            var actual = DatabaseSetup.UserService.GetAll().FirstOrDefault(x => x.Login == "userfive");

            //Assert
            Assert.AreEqual("userfive", actual.Login, "Não foi possível salvar o usuário.");
        }
        /// <summary>
        /// Verifica se o método Update consegue atualizar um usuário.
        /// </summary>
        [TestMethod]
        public void UpdateUser()
        {
            //Arrange
            var user = DatabaseSetup.UserService.GetByLogin("usertwo");

            //Act
            user.IsActive = false;
            DatabaseSetup.UserService.Update(user);
            var userUpdated = DatabaseSetup.UserService.GetByLogin("usertwo");

            //Assert
            Assert.AreEqual(false, userUpdated.IsActive, "Não foi possível alterar o usuário.");
        }
        /// <summary>
        /// Testa o método de Delete. Verifica se é possível excluir um usuário.
        /// </summary>
        [TestMethod]
        public void DeleteUser()
        {
            //Arrange
            var user = DatabaseSetup.UserService.GetByLogin("userthree");

            // Act
            DatabaseSetup.UserService.Delete(user);

            var userDeleted = DatabaseSetup.UserService.GetByLogin("userthree");
            //Assert
            Assert.IsNull(userDeleted, "Não foi possível excluir o usuário.");
        }

        #endregion

        #region PostService Tests

        /// <summary>
        /// Verifica se GetAll retorna posts.
        /// </summary>
        [TestMethod]
        public void GetPosts()
        {
            //Arrange
            //Act
            IEnumerable<Post> expected = DatabaseSetup.PostService.GetAll();

            //Assert
            Assert.IsTrue(expected.Count() > 0, "Nenhum resultado obtido.");
        }
        /// <summary>
        /// Verifica GetById se é possível retornar um post.
        /// </summary>
        [TestMethod]
        public void GetPostById()
        {
            //Arrange
            int postId = DatabaseSetup.PostService.GetAll().First().PostId;

            // Act
            var result = DatabaseSetup.PostService.GetById(postId);

            //Assert
            Assert.IsNotNull(result, "Não foi possível obter o post.");
        }
        /// <summary>
        /// Testa Save. Se é possível adicionar um post.
        /// </summary>
        [TestMethod]
        public void SavePost()
        {
            //Arrange
            var post = new Post
            {
                Commentaries = new Collection<PostCommentary>(),
                EditDate = new DateTime(2014, 11, 20),
                IsActive = true,
                PostContent = "Content",
                PublishDate = new DateTime(2014, 11, 20),
                Summary = "Summary",
                Tags = "tags",
                Title = "Post 6",
                User = DatabaseSetup.UserService.GetByLogin("userfour")
            };

            // Act
            DatabaseSetup.PostService.Save(post);
            var actual = DatabaseSetup.PostService.GetAll().FirstOrDefault(x => x.Title == "Post 6");

            //Assert
            Assert.AreEqual("Post 6", actual.Title, "Não foi possível salvar o post.");
        }
        /// <summary>
        /// Verifica Update. Testa a atualização de post.
        /// </summary>
        [TestMethod]
        public void UpdatePost()
        {
            //Arrange
            var post = DatabaseSetup.PostService.GetAll().FirstOrDefault(x => x.Title == "Post 1");

            //Act
            post.PostContent = "Modified";
            DatabaseSetup.PostService.Update(post);
            var postUpdated = DatabaseSetup.PostService.GetAll().FirstOrDefault(x => x.Title == "Post 1");

            //Assert
            Assert.AreEqual("Modified", postUpdated.PostContent, "Não foi possível alterar o post.");
        }
        /// <summary>
        /// Testa Delete. Verifica se é possíve excluir um post.
        /// </summary>
        [TestMethod]
        public void DeletePost()
        {
            //Arrange
            var post = DatabaseSetup.PostService.GetAll().FirstOrDefault(x => x.Title == "Post 1");

            // Act
            DatabaseSetup.PostService.Delete(post);

            var postDeleted = DatabaseSetup.PostService.GetAll().FirstOrDefault(x => x.Title == "Post 1");
            //Assert
            Assert.IsNull(postDeleted, "Não foi possível excluir o post.");
        }
        /// <summary>
        /// Testa GetPostsPaginated. Verifica se retorna resultados.
        /// </summary>
        [TestMethod]
        public void GetPostsPaginated()
        {
            //Arrange
            var page = 1;
            var pageSize = 4;

            // Act
            var posts = DatabaseSetup.PostService.GetPostsPaginated(page, pageSize);

            // Assert
            Assert.IsTrue(posts.Count() > 0, "Nenhum resultado obtido.");
        }
        /// <summary>
        /// Testa GetPostsByUserId. Verifica se é possível obter posts por usuário.
        /// </summary>
        [TestMethod]
        public void GetPostsByUserId()
        {
            //Arrange
            var page = 1;
            var pageSize = 4;
            var userId = DatabaseSetup.UserService.GetAll().FirstOrDefault(x => x.Login == "userone").UserId;

            // Act
            var posts = DatabaseSetup.PostService.GetPostsByUserId(page, pageSize, userId);

            // Assert
            Assert.IsTrue(posts.Count() > 0, "Nenhum resultado obtido.");
        }

        #endregion
    }
}
