using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using CodeWarriors.IITDU.Models;
using CodeWarriors.IITDU.OAuth;
using CodeWarriors.IITDU.Repository;
using CodeWarriors.IITDU.Service;

namespace CodeWarriors.IITDU.Controllers
{
    public class OAuthController : Controller
    {
        private GoogleOAuthClient _googleOAuthClient;
        private String _googleRedirectUrl;
        private AccountService _accountService;

        public OAuthController()
        {
            _googleOAuthClient = new GoogleOAuthClient("1056467039068-ufmdhjpcnipos6vjud2ai9glthq267id.apps.googleusercontent.com", "s7MbBHZZYEF_sBGBOnsyVbOg");
            _googleRedirectUrl = "http://localhost:33754/OAuth/GoogleSignInSuccess";
            _accountService = new AccountService(new UserRepository(new DatabaseContext()), new ProfileRepository(new DatabaseContext()));
        }

        //public ActionResult DeleteDB()
        //{
        //    DatabaseContext databaseContext=new DatabaseContext();
        //    databaseContext.Database.Delete();
        //    databaseContext.Database.Create();
        //    return Redirect("http://www.google.com.bd");
        //}
        //
        // GET: /OAuth/
        public ActionResult GoogleSignin()
        {
            var url = _googleOAuthClient.GetSignInUrl("", _googleRedirectUrl);
            return Redirect(url);
        }

        public ActionResult GoogleSignInSuccess([FromUri]OAuthResponseParameters parameters)
        {
            if (!parameters.HasError)
            {
                var oAuthTokenResponse = _googleOAuthClient.GetTokenResponse(parameters.Code, _googleRedirectUrl);

                var userInfo = _googleOAuthClient.GetUserInfo(oAuthTokenResponse.AccessToken);

                if (_accountService.GetUser(userInfo.UniqueId)==null)
                {
                    var user = new User { Email = userInfo.UniqueId, Password = "iit123"};
                    _accountService.SaveUser(user);

                    var newUserProfile = new UserProfile
                    {
                        Email = userInfo.UniqueId,
                        FirstName = userInfo.FullName,
                        Gender = userInfo.Gender
                    };
                    _accountService.SaveProfile(newUserProfile, user.Email);
                }

                FormsAuthentication.SetAuthCookie(userInfo.UniqueId, false);
             
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
