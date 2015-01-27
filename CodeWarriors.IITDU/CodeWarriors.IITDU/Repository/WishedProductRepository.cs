using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;

namespace CodeWarriors.IITDU.Repository
{
    public class WishedProductRepository:IRepository<WishedProduct>
    {
        private readonly DatabaseContext _databaseContext;
        public WishedProductRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public bool Add(WishedProduct model)
        {
            _databaseContext.WishedProducts.Add(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Remove(WishedProduct model)
        {
            _databaseContext.WishedProducts.Remove(model);
            return _databaseContext.SaveChanges() > 0;
        }
        public bool RemoveById(int id)
        {
            var model = _databaseContext.WishedProducts.FirstOrDefault(e => e.WishedProductId == id);
            return Remove(model);
        }
        public bool Update(WishedProduct model)
        {
            throw new NotImplementedException();
        }

        public List<WishedProduct> GetAll()
        {
            return _databaseContext.WishedProducts.ToList();
        }

        public WishedProduct Get(int id)
        {
            return _databaseContext.WishedProducts.Find(id);
        }

        public List<WishedProduct> GetAllByUserId(int userId)
        {
            return _databaseContext.WishedProducts.Where(w => w.UserId == userId).ToList();
        }
    }
}