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

        public List<User> GetAllUser(String subCatagoryName)
        {
            UserRepository userProfileRepository = new UserRepository(new DatabaseContext());
            var sql = from wishedProduct in _databaseContext.WishedProducts
                      join product in _databaseContext.Products on wishedProduct.ProductId equals product.ProductId
                      where product.SubCatagoryName.Equals(subCatagoryName)
                      select new
                      {
                          UserId = wishedProduct.UserId
                      }
            ;

            List<User> userProfiles = new List<User>();
            foreach (var userId in sql)
            {
                userProfiles.Add(userProfileRepository.Get(userId.UserId));
            }
            return userProfiles;

        }

        //story number 16
        //public List<Product>GetTopRatedProducts(int userId)
        //{
        //    var sql = from wishedProduct in _databaseContext.WishedProducts
        //              join product in _databaseContext.Products on wishedProduct.SellerId equals product.SellerId
        //              select product.SubCatagoryName;
            
        //    List<Product>topRatedProducts=new List<Product>();

        //    foreach (var s in sql)
        //    {
                
        //    }


        //}

        public List<Product> GetTopRatedProducts(int userId)
        {
            var SubCatagoryNames = from wishedProduct in _databaseContext.WishedProducts
                                   join product in _databaseContext.Products on wishedProduct.ProductId equals
                                       product.ProductId
                                   select product.SubCatagoryName;

            List<Product> topRatedProducts = new List<Product>();

            foreach (var subCatagoryName in SubCatagoryNames)
            {
                var products = from product in _databaseContext.Products
                               where product.SubCatagoryName.Equals(subCatagoryName)
                               select product;
                topRatedProducts.AddRange(products.Distinct().ToList());
            }

            return topRatedProducts.OrderByDescending(model => model.AverageRate).Distinct().ToList();


        }

    }
}