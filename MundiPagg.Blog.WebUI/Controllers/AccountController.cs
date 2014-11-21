using System;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using MundiPagg.Blog.Service;
using MundiPagg.Blog.WebUI.CustomController;
using MundiPagg.Blog.WebUI.CustomSessionControl;
using MundiPagg.Blog.WebUI.Models;
using Ninject;

namespace MundiPagg.Blog.WebUI.Controllers
{
    /// <summary>
    /// Classe de controller para login e registro.
    /// </summary>
    public class AccountController : BlogController
    {
        /// <summary>
        /// Realização da injeção do serviço na instância da classe.
        /// </summary>
        [Inject]
        public UserService UserService { get; set; }

        #region Actions

        /// <summary>
        /// Exibição da págian de login.
        /// </summary>
        /// <returns>View LogOn</returns>
        public ActionResult LogOn()
        {
            return View();
        }
        /// <summary>
        /// Processa a requsição de login no sistema
        /// </summary>
        /// <param name="model">Model com os dados do formulário de login</param>
        /// <param name="returnUrl"></param>
        /// <returns>Um redirecionamento para a View Index da HomeController
        /// ou a View LogOn em caso de falha de login.</returns>
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        SetUserSession(model.UserName);
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        SetUserSession(model.UserName);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Usuário e / ou senha incorretos.");
                }
            }

            return View(model);
        }
        /// <summary>
        /// Processa a requisição de logoff do site.
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            // Limpa a sessão de usuário.
            SessionHelper.CleanSession();

            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// Exibe a tela de registro.
        /// </summary>
        /// <returns>View Register</returns>
        public ActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// Processa a requisição de envio do formulário de registro de usuário.
        /// </summary>
        /// <param name="model">Model data com os campos do formulário.</param>
        /// <returns>Um redirecionamento para a View da HomeController ou 
        /// a View Register em caso de falha no registro.</returns>
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Criação do usuário.
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    // Adicona usuário a Users
                    AddUser(model.UserName, model.Email);
                    // Adiciona na sessão.
                    SetUserSession(model.UserName);
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            return View(model);
        }
        /// <summary>
        /// Obtém o usuário logado pela Id do repositório de Users e cria-se
        /// uma referência para ele na sessão e adiciona na classe de controle de
        /// sessão de usuários.
        /// </summary>
        /// <param name="userName">login do usuário</param>
        private void SetUserSession(string userName)
        {
            var user = UserService.GetByLogin(userName);
            SessionHelper.User = Mapper.Map<MundiPagg.Blog.Domain.Entities.User>(user);
        }
        /// <summary>
        /// Adiciona o usuário a entidade User do banco do Blog.
        /// e o salva.
        /// </summary>
        /// <param name="userName">Login</param>
        /// <param name="email">Email</param>
        private void AddUser(string userName, string email)
        {
            
            var user = new MundiPagg.Blog.Domain.Entities.User
            {
                Login = userName,
                Email = email,
                FirstName = userName,
                LastName = string.Empty,
                IsAdmin = false,
                IsActive = true,
                AboutMe = string.Empty,
                DateRegistered = DateTime.Now
            };

            UserService.Save(user);
        }

        #endregion

        #region Not implemented

        //
        // GET: /Account/ChangePassword

        //[Authorize]
        //public ActionResult ChangePassword()
        //{
        //    return View();
        //}

        //
        // POST: /Account/ChangePassword

        //[Authorize]
        //[HttpPost]
        //public ActionResult ChangePassword(ChangePasswordModel model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        // ChangePassword will throw an exception rather
        //        // than return false in certain failure scenarios.
        //        bool changePasswordSucceeded;
        //        try
        //        {
        //            MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
        //            changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
        //        }
        //        catch (Exception)
        //        {
        //            changePasswordSucceeded = false;
        //        }

        //        if (changePasswordSucceeded)
        //        {
        //            return RedirectToAction("ChangePasswordSuccess");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //
        // GET: /Account/ChangePasswordSuccess

        //public ActionResult ChangePasswordSuccess()
        //{
        //    return View();
        //}

        #endregion

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
