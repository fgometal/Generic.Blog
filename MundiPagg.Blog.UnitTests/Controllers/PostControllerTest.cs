using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MundiPagg.Blog.WebUI.Controllers;
using MundiPagg.Blog.WebUI.Models;
using MundiPagg.Blog.WebUI.UnitTests.DatabaseContext;

namespace MundiPagg.Blog.WebUI.UnitTests.Controllers
{
    /// <summary>
    /// Classe de teste para a PostController
    /// </summary>
    [TestClass]
    public class PostControllerTest
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

        #region Controller Tests

        /// <summary>
        /// Testa a Index. VErifica se é possível exibir a view.
        /// </summary>
        [TestMethod]
        public void Index()
        {
            // Arrange
            var postId = DatabaseSetup.PostService.GetAll().First().PostId;
            var controller = new PostController();
            controller.PostService = DatabaseSetup.PostService;

            // Act
            var result = ((ViewResult)controller.Index(postId)).Model;

            // Assert
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// Testa a Edit. Verifica se é possível exibir a view.
        /// </summary>
        [TestMethod]
        public void Edit()
        {
            // Arrange
            var postId = DatabaseSetup.PostService.GetAll().First().PostId;
            var controller = new PostController();
            controller.PostService = DatabaseSetup.PostService;

            // Act
            var result = ((ViewResult)controller.Edit(postId)).Model;

            // Assert
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// Testa a List. Verifica se é possível exibir a lista de posts.
        /// </summary>
        [TestMethod]
        public void List()
        {
            // Arrange
            var user = DatabaseSetup.UserService.GetAll().FirstOrDefault(x => x.Login == "userone");
            int page = 1;
            var controller = new PostController();
            controller.PostService = DatabaseSetup.PostService;
            controller.CurrentUser = user;

            // Act
            var result = controller.List(page) as ViewResult;
            var model = (IndexViewModel)result.ViewData.Model;

            //Assert
            PostModel[] postArray = model.Posts.ToArray();
            Assert.IsTrue(postArray.Length == 2, "Não foi possível obter resultados.");
            Assert.AreEqual(postArray[0].Title, "Post 3", "Os objetos comparados não correspondem.");
            Assert.AreEqual(postArray[1].Title, "Post 2", "Os objetos comparados não correspondem.");
        }
        /// <summary>
        /// Testa a action para Delete. Verifica se é possível excluir.
        /// </summary>
        [TestMethod]
        public void Delete()
        {
            //Arrange
            var postId = DatabaseSetup.PostService.GetAll().First().PostId;
            var controller = new PostController();
            controller.PostService = DatabaseSetup.PostService;

            // Act
            controller.Delete(postId);
            var tempData = "Seu post foi excluído com sucesso.";

            // Arrange
            Assert.AreEqual(controller.TempData["Notification"], tempData, "Não foi possível excluir o post."); 
        }

        #endregion
    }
}
