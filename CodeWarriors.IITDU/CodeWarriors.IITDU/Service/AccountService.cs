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

        public UserProfile GetProfile(string userName)
        {
            int userId = _userRepository.GetUserId(userName);
            return _profileRepository.Get(userId);
        }

        public User GetUser(string userName)
        {
            return _userRepository.GetUser(userName);
        }

        public void UpdateAccount(User user)
        {
            _userRepository.Update(user, _userRepository.GetUserId(user.UserName));
        }

        public bool ValidatePassword(string userName, string password)
        {
            var user = _userRepository.GetUser(userName);
            return user.Password == password;
        }

        public void EditProfile(UserProfile userProfile, string userName)
        {
            _profileRepository.Update(userProfile, _userRepository.GetUserId(userName));
        }
    }
}