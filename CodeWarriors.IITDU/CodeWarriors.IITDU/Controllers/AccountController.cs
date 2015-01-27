using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CodeWarriors.IITDU.Models;
using CodeWarriors.IITDU.Repository;
using CodeWarriors.IITDU.Service;
using CodeWarriors.IITDU.ViewModels;

namespace CodeWarriors.IITDU.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        private User _user;
        private UserProfile _profile;
        private EditAccountModel _editAccountModel;
        public AccountController(AccountService accountService, User user, UserProfile profile, EditAccountModel editAccountModel)
        {
            _accountService = accountService;
            _user = user;
            _profile = profile;
            _editAccountModel = editAccountModel;
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/#/home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                bool status = _accountService.ValidateLogin(model.UserName, model.Password);
                if (status)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    ViewData["Status"] = "Login Successful";
                    Session["CartList"] = new List<CartItem>();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session["Error"] = "User Name/ Password Mismatch";
                    ViewData["Status"] = "Login Failure";
                    return Redirect("/#/login");
                }

            }
            return Redirect("/#/login");
        }
        [HttpGet]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/#/home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                bool status = _accountService.ValidateRegistration(model.UserName);
                if (status)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    _user.Password = model.Password;
                    _user.UserName = model.UserName;
                    _accountService.SaveUser(_user);
                    _profile.FirstName = model.FirstName;
                    _profile.LastName = model.LastName;
                    _accountService.SaveProfile(_profile, model.UserName);
                    ViewData["Status"] = "Registration Successful";
                    Session["CartList"] = new List<CartItem>();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "User Name already exists");
                    ViewData["Status"] = "Registration Failure";
                    return View();
                }

            }
            return View();
        }
        [Authorize]
        public ActionResult ChangePassword()
        {
            var user = _accountService.GetUser(User.Identity.Name);
            _editAccountModel.UserName = user.UserName;
            return View(_editAccountModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword([Bind(Exclude = "UserName")]EditAccountModel model)
        {
            model.UserName = User.Identity.Name;
            if (ModelState.IsValid)
            {
                if (_accountService.ValidateLogin(model.UserName, model.CurrentPassword))
                {
                    _user.UserName = model.UserName;
                    _user.Password = model.Password;
                    _accountService.UpdateAccount(_user);
                    Session["Success"] = "Edit account successful";
                }
                else
                {
                    Session["Error"] = "Current Password does not match";
                }
            }

            return Redirect("/#/password");
        }
        [Authorize]
        public ActionResult LogOut()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public ActionResult IsAuthenticated()
        {
            return Json(User.Identity.IsAuthenticated,JsonRequestBehavior.AllowGet);
        }

    }
}
