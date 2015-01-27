using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CodeWarriors.IITDU.Models;

namespace CodeWarriors.IITDU.Repository
{
    public class CategoryRepository : IRepository<Category>
    {
        private DatabaseContext _databaseContext;
        public CategoryRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public bool Add(Category model)
        {
            _databaseContext.Catagories.Add(model);
            _databaseContext.SaveChanges();
            return true;
        }

        public bool Remove(Category model)
        {
            if (!IsCatagoryExists(model.CatagoryId))
                return false;
            _databaseContext.Catagories.Remove(model);
            return true;
        }

        public bool Update(Category model)
        {
            if (!IsCatagoryExists(model.CatagoryId))
                return false;
            _databaseContext.Entry(model).State=EntityState.Modified;
            _databaseContext.SaveChanges();
            return true;
        }

        public List<Category> GetAll()
        {
            var query=from catagory in _databaseContext.Catagories select catagory;
            return query.ToList();
        }

        public Category Get(int modelId)
        {
            if (!IsCatagoryExists(modelId)) 
                return new NullCategory();
            var query = from catagory in _databaseContext.Catagories where catagory.CatagoryId.Equals(modelId) select catagory;
            return query.First();
        }

        private bool IsCatagoryExists(int catagoryId)
        {
            return _databaseContext.Catagories.Any(catagory => catagory.CatagoryId.Equals(catagoryId));
        }

        public int GetCategoryIdByCategoryName(string  name)
        {
            var category = _databaseContext.Catagories.FirstOrDefault(c => c.Name.ToLower() == name.ToLower());
            if(category==null)
            {
                return 0;
            }
            return category.CatagoryId;
        }

    }
}