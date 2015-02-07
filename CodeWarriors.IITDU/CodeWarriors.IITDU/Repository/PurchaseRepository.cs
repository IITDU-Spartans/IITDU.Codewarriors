using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;

namespace CodeWarriors.IITDU.Repository
{
    public class PurchaseRepository : IRepository<Purchase>
    {
        private readonly DatabaseContext _databaseContext;
        public PurchaseRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public bool Add(Purchase model)
        {
            _databaseContext.Purchases.Add(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Remove(Purchase model)
        {
            _databaseContext.Purchases.Remove(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Update(Purchase model)
        {
            _databaseContext.Entry(model).State = EntityState.Modified;
            return _databaseContext.SaveChanges() > 0;
        }

        public List<Purchase> GetAll()
        {
            return _databaseContext.Purchases.ToList();
        }

        public Purchase Get(int id)
        {
            return _databaseContext.Purchases.Find(id);
        }

        public List<Product> GetTopRatedProducts(int userId)
        {
            var subCatagoryNames = from purchase in _databaseContext.Purchases
                                   join product in _databaseContext.Products on purchase.ProductId equals
                                       product.ProductId
                                   select product.SubCatagoryName;

            List<Product> topRatedProducts = new List<Product>();

            foreach (var subCatagoryName in subCatagoryNames)
            {
                var products = from product in _databaseContext.Products
                               where product.SubCatagoryName.Equals(subCatagoryName)
                               select product;

                topRatedProducts.AddRange(products.ToList());
            }

            return topRatedProducts.OrderByDescending(model => model.AverageRate).Distinct().ToList();
        }
    }
}