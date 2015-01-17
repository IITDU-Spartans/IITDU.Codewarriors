using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;

namespace CodeWarriors.IITDU.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly UserContext _userContext;
        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public User GetUser(String userName)
        {
            return _userContext.Users.FirstOrDefault(u => u.UserName == userName);
        }


        public bool Insert(User user)
        {
            _userContext.Users.Add(user);
            return _userContext.SaveChanges() > 0;
        }

        public bool Update(User user, int id)
        {
            var oldUser = Get(id);
            oldUser.Password = user.Password;
            _userContext.Entry(oldUser).State = EntityState.Modified;
            return _userContext.SaveChanges() > 0;
        }

        public User Get(int userId)
        {
            return _userContext.Users.Find(userId);
        }

        public int GetUserId(string userName)
        {
            var user = GetUser(userName);
            return user.UserId;
        }
    }
}