using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;

namespace CodeWarriors.IITDU.Repository
{
    public class ReviewRepository:IRepository<Review>
    {
        private readonly DatabaseContext _databaseContext;
        public ReviewRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public bool Add(Review model)
        {
            _databaseContext.Reviews.Add(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Remove(Review model)
        {
            _databaseContext.Reviews.Remove(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Update(Review model)
        {
            _databaseContext.Entry(model).State = EntityState.Modified;
            return _databaseContext.SaveChanges() > 0;
        }

        public List<Review> GetAll()
        {
            return _databaseContext.Reviews.ToList();
        }

        public Review Get(int id)
        {
            return _databaseContext.Reviews.Find(id);
        }

        public List<Review> GetReviewsByProductId(int productId)
        {
            return _databaseContext.Reviews.Where(e => e.ProductId == productId).ToList();
        }
    }
}