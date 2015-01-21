using CodeWarriors.IITDU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeWarriors.IITDU.Service
{
    public interface IAccountService
    {
        bool ValidateLogin(string userName, string password);
        bool ValidateRegistration(string userName);
        UserProfile GetProfile(string userName);
        User GetUser(string userName);
        void SaveUser(User user);
        void UpdateAccount(User user);
        void EditProfile(UserProfile userProfile, string userName);

    }
}