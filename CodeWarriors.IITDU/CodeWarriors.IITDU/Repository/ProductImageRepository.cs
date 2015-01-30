using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;

namespace CodeWarriors.IITDU.Repository
{
    public class ProductImageRepository : IRepository<ProductImage>
    {
        private DatabaseContext _databaseContext;
        public ProductImageRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }


        public bool Add(ProductImage model)
        {
            _databaseContext.ProductImages.Add(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Remove(ProductImage model)
        {
            _databaseContext.ProductImages.Remove(model);
            return _databaseContext.SaveChanges() > 0;
        }
        public bool Remove(int id)
        {
            var model = Get(id);
            _databaseContext.ProductImages.Remove(model);
            return _databaseContext.SaveChanges() > 0;
        }
        public bool Update(ProductImage model)
        {
            _databaseContext.Entry(model).State = EntityState.Modified;

            return _databaseContext.SaveChanges() > 0;
        }

        public List<ProductImage> GetAll()
        {
            return _databaseContext.ProductImages.ToList();
        }

        public ProductImage Get(int id)
        {
            return _databaseContext.ProductImages.Find(id);
        }

        public List<ProductImage> GetImagesByProductId(int id)
        {
            return _databaseContext.ProductImages.Where(e => e.ProductId == id).ToList();
        }

        public bool RemoveAllImagesByProductId(int productId)
        {
            var models = _databaseContext.ProductImages.Where(e => e.ProductId == productId).ToList();
            foreach (var model in models)
            {
                Remove(model);
            }
            return _databaseContext.SaveChanges() > 0;
        }
    }
}