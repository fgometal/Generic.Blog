using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Service;
using MundiPagg.Blog.WebUI.Controllers;
using MundiPagg.Blog.WebUI.Models;
using MundiPagg.Blog.WebUI.UnitTests.DatabaseContext;

namespace MundiPagg.Blog.WebUI.UnitTests.Controllers
{
    /// <summary>
    /// Classe de teste para AccountController
    /// </summary>
    [TestClass]
    public class AccountControllerTest
    {
        #region Tests Initialization

        /// <summary>
        /// Inicialização que ocorre antes de cada teste.
        /// </summary>
        [TestInitialize]
        public void PreTestInitialize()
        {
            var appDataPath = System.IO.Directory.GetCurrentDirectory().Substring(0, System.IO.Directory.GetCurrentDirectory().Length - 9) + "App_Data";
            AppDomain.CurrentDomain.SetData("DataDirectory", appDataPath);
            // Inicializa a base de testes.
            //DatabaseSetup.DatabaseStartUp();
        }

        #endregion

        /// <summary>
        /// Realiza os testes de exibição da View de LogOn
        /// </summary>
        [TestMethod]
        public void LogOn()
        {
            //Arrange
            var controller = new AccountController();

            //Act
            var result = controller.LogOn() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// Testa o logon de usuário.
        /// </summary>
        [TestMethod]
        public void LogOn_Login()
        {
            // Arrange
            var controller = new AccountController();

            var model = new LogOnModel
            {
                UserName = "test",
                Password = "#abc123"
            };

            var returnUrl = "/Home/Index";

            // Act
            var result = controller.LogOn(model, returnUrl) as RedirectResult;

            // Assert
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// Testa o logoff de usuários
        /// </summary>
        [TestMethod]
        public void LogOff()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Testa a exibição da tela de registro
        /// </summary>
        [TestMethod]
        public void Register()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Testa p processo de registro de usuário.
        /// </summary>
        [TestMethod]
        public void Register_RegisterUser()
        {
            throw new NotImplementedException();
        }
    }
}
