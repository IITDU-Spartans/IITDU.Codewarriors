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
        private readonly  AccountService _accountService;
        private User _user;
        private UserProfile _profile;
        public AccountController(AccountService accountService, User user, UserProfile profile)
        {
            _accountService = accountService;
            _user = user;
            _profile = profile;
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
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("","User Name/ Password Mismatch");
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
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
