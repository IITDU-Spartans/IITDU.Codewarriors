using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;

namespace CodeWarriors.IITDU.Repository
{
    public class ProfileRepository : IRepository<UserProfile>
    {
        private readonly DatabaseContext _userContext;
        public ProfileRepository(DatabaseContext userContext)
        {
            _userContext = userContext;
        }

        public bool Add(UserProfile profile)
        {
            _userContext.UserProfiles.Add(profile);
            return _userContext.SaveChanges() > 0;
        }

        public bool Remove(UserProfile model)
        {
            throw new NotImplementedException();
        }

        public bool Update(UserProfile model)
        {
            throw new NotImplementedException();
        }

        public List<UserProfile> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(UserProfile profile, int id)
        {
            var oldProfile = _userContext.UserProfiles.Find(id);
            oldProfile.FirstName = profile.FirstName;
            oldProfile.LastName = profile.LastName;
            oldProfile.Gender = profile.Gender;
            oldProfile.Email = profile.Email;
            oldProfile.Country = profile.Country;
            oldProfile.Age = profile.Age;
            oldProfile.About = profile.About;
            _userContext.Entry(oldProfile).State = EntityState.Modified;
            return _userContext.SaveChanges() > 0;
        }

        public UserProfile Get(int userId)
        {
            return _userContext.UserProfiles.FirstOrDefault(u => u.UserId == userId);
        }
    }
}