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
        private readonly DatabaseContext _userContext;
        public UserRepository(DatabaseContext userContext)
        {
            _userContext = userContext;
        }

        public User GetUser(String userName)
        {
            return _userContext.Users.FirstOrDefault(u => u.UserName == userName);
        }


        public bool Add(User user)
        {
            _userContext.Users.Add(user);
            return _userContext.SaveChanges() > 0;
        }

        public bool Remove(User model)
        {
            throw new NotImplementedException();
        }

        public bool Update(User model)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
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