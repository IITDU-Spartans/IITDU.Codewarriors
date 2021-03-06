﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;
using CodeWarriors.IITDU.Repository;
using CodeWarriors.IITDU.Utility;

namespace CodeWarriors.IITDU.Service
{
    public class AccountService : IAccountService
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
                return user.Password == Encryption.GetHash(password);
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
            user.Password = Encryption.GetHash(user.Password);
            _userRepository.Add(user);
        }
        public void SaveProfile(UserProfile profile, string userName)
        {
            profile.UserId = _userRepository.GetUserId(userName);
            _profileRepository.Add(profile);
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
            user.Password = Encryption.GetHash(user.Password);
            _userRepository.Update(user, _userRepository.GetUserId(user.Email));
        }

        public void EditProfile(UserProfile userProfile, string userName)
        {
            _profileRepository.Update(userProfile, _userRepository.GetUserId(userName));
        }

        public UserProfile GetUserProfileByUserId(int userId)
        {
           return _profileRepository.Get(userId);
           
        }
    }
}