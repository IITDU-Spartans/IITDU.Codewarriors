using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;
using CodeWarriors.IITDU.Repository;

namespace CodeWarriors.IITDU.Service
{
    public class AccountService
    {
        private readonly UserRepository _userRepository;
        private readonly ProfileRepository _profileRepository;
        public AccountService(UserRepository userRepository, ProfileRepository profileRepository)
        {
            _userRepository = userRepository;
            _profileRepository = profileRepository;
        }
        public bool ValidateLogin(string userName, string password)
        {
            var user = _userRepository.GetUser(userName);
            if (user != null)
            {
                return user.Password == password;
            }
            return false;
        }

        public bool ValidateRegistration(string userName)
        {
            var user = _userRepository.GetUser(userName);
            return user == null;
        }

        public void SaveUser(User user)
        {
            _userRepository.Insert(user);
        }
        public void SaveProfile(UserProfile profile, string userName)
        {
            profile.UserId = _userRepository.GetUserId(userName);
            _profileRepository.Insert(profile);
        }
    }
}