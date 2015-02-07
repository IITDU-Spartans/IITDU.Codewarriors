using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeWarriors.IITDU.Models;
using CodeWarriors.IITDU.Service;
using CodeWarriors.IITDU.ViewModels;

namespace CodeWarriors.IITDU.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private AccountService _accountService;
        private ProfileModel _profileModel;
        private UserProfile _profile;
        public ProfileController(AccountService accountService, ProfileModel profileModel, UserProfile profile)
        {
            _accountService = accountService;
            _profileModel = profileModel;
            _profile = profile;
        }
        public ActionResult Index()
        {
            var profile = _accountService.GetProfile(User.Identity.Name);
            _profileModel.FirstName = profile.FirstName;
            _profileModel.LastName = profile.LastName;
            _profileModel.About = profile.About;
            _profileModel.Age = profile.Age;
            _profileModel.Gender = profile.Gender;
            _profileModel.Location = profile.Location;

            _profileModel.ImageUrl = profile.ImageUrl;
            return View(_profileModel);
        }

        [HttpGet]
        public JsonResult GetProfileInformation()
        {
            var profileInformation = _accountService.GetProfile(User.Identity.Name);
            return Json(profileInformation, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetProfileInformationByUserId(int userId)
        {
            var profileInformation = _accountService.GetUserProfileByUserId(userId);
            return Json(profileInformation, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateProfile(ProfileModel model)
        {
            if (ModelState.IsValid)
            {
                _profile.FirstName = model.FirstName;
                _profile.LastName = model.LastName;
                _profile.Age = model.Age;
                _profile.About = model.About;
                _profile.Location = model.Location;
                _profile.MobileNumber = model.MobileNumber;
                _profile.Gender = model.Gender;
                if (model.ImageUrl.Contains("Upload/"))
                    _profile.ImageUrl = model.ImageUrl;
                else
                    _profile.ImageUrl = "Upload/" + model.ImageUrl;
                _accountService.EditProfile(_profile, User.Identity.Name);
                return Json("Updated Successfully", JsonRequestBehavior.AllowGet);
            }
            return Json("Sorry, Could Not Update", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateProfilePicture(ProfileModel model)
        {
            _profile.FirstName = model.FirstName;
            _profile.LastName = model.LastName;
            _profile.Age = model.Age;
            _profile.About = model.About;
            _profile.Location = model.Location;
            _profile.MobileNumber = model.MobileNumber;
            _profile.Gender = model.Gender;
            if (model.ImageUrl.Contains("Upload/"))
                _profile.ImageUrl = model.ImageUrl;
            else
                _profile.ImageUrl = "Upload/" + model.ImageUrl;
            _accountService.EditProfile(_profile, User.Identity.Name);
            return Json("Updated Successfully", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit()
        {
            var profile = _accountService.GetProfile(User.Identity.Name);
            _profileModel.FirstName = profile.FirstName;
            _profileModel.LastName = profile.LastName;
            _profileModel.About = profile.About;
            _profileModel.Age = profile.Age;
            _profileModel.Gender = profile.Gender;
            _profileModel.Location = profile.Location;
            _profileModel.MobileNumber = profile.MobileNumber;
            _profileModel.ImageUrl = profile.ImageUrl;
            return View(_profileModel);
        }
        [HttpPost]
        public ActionResult Edit(ProfileModel model)
        {
            if (ModelState.IsValid)
            {
                _profile.FirstName = model.FirstName;
                _profile.LastName = model.LastName;
                _profile.Age = model.Age;
                _profile.About = model.About;
                _profile.Location = model.Location;
                _profile.MobileNumber = model.MobileNumber;
                _profile.Gender = model.Gender;
                _accountService.EditProfile(_profile, User.Identity.Name);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
