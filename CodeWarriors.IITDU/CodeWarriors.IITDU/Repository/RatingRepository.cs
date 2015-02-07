using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;

namespace CodeWarriors.IITDU.Repository
{
    public class RatingRepository : IRepository<Rating>
    {
        private readonly DatabaseContext _databaseContext;
        public RatingRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public bool Add(Rating model)
        {
            _databaseContext.Ratings.Add(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Remove(Rating model)
        {
            _databaseContext.Ratings.Remove(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Update(Rating model)
        {
            _databaseContext.Entry(model).State = EntityState.Modified;
            return _databaseContext.SaveChanges() > 0;
        }

        public List<Rating> GetAll()
        {
            return _databaseContext.Ratings.ToList();
        }

        public Rating Get(int id)
        {
            return _databaseContext.Ratings.FirstOrDefault(r => r.RatingId == id);
        }

        public Rating GetRatingByUserId(int userId)
        {
            return _databaseContext.Ratings.FirstOrDefault(e => e.BuyerId == userId);
        }
    }
}