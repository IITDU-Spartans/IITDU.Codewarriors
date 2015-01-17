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
                    return RedirectToAction("Index", "Profile");
                }
                else
                {
                    ModelState.AddModelError("", "User Name/ Password Mismatch");
                    ViewData["Status"] = "Login Failure";
                    return View();
                }

            }
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
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
                    return RedirectToAction("Index", "Profile");
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
        public ActionResult Edit()
        {
            var user = _accountService.GetUser(User.Identity.Name);
            _editAccountModel.UserName = user.UserName;
            return View(_editAccountModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "UserName")]EditAccountModel model)
        {
            model.UserName = User.Identity.Name;
            if (ModelState.IsValid)
            {
                if (_accountService.ValidatePassword(model.UserName, model.CurrentPassword))
                {
                    _user.UserName = model.UserName;
                    _user.Password = model.Password;
                    _accountService.UpdateAccount(_user);
                    ViewBag.Success = "Edit account successful";
                }
                else
                {
                    ModelState.AddModelError("","Current Password does not match");
                }
            }

            return View(model);
        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}
