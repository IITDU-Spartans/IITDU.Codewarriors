using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.DynamicData;
using CodeWarriors.IITDU.Models;

namespace CodeWarriors.IITDU.Repository
{
    public class ProductRepository : IProductRepository, IRepository<Product>
    {
        private readonly DatabaseContext _databaseContext;
        public ProductRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public int AddProduct(Product product)
        {
            _databaseContext.Products.Add(product);
            _databaseContext.SaveChanges();
            return product.ProductId;
        }

        public bool RemoveProduct(Product product)
        {
            if (!IsProductExists(product.ProductId))
                return false;
            _databaseContext.Products.Remove(product);
            _databaseContext.SaveChanges();
            return true;
        }

        public bool UpdateProduct(Product product)
        {
            if (!IsProductExists(product.ProductId))
                return false;
            _databaseContext.Entry(product).State = EntityState.Modified;
            _databaseContext.SaveChanges();
            return true;
        }

        public List<Product> GetAllProduct()
        {
            return _databaseContext.Products.Select(product => product).ToList();
        }

        public Product GetProductByProductId(int productId)
        {
            if (!IsProductExists(productId))
                return new NullProduct();
            var query = from product in _databaseContext.Products where product.ProductId.Equals(productId) select product;
            return query.First();

        }

        private bool IsProductExists(int productId)
        {
            return _databaseContext.Products.Any(product => product.ProductId.Equals(productId));
        }

        public bool Add(Product model)
        {
            return AddProduct(model) > 0;
        }

        public bool Remove(Product model)
        {
            return RemoveProduct(model);
        }

        public bool Update(Product model)
        {
            return UpdateProduct(model);
        }

        public List<Product> GetAll()
        {
            return GetAllProduct();
        }

        public Product Get(int modelId)
        {
            return GetProductByProductId(modelId);
        }


        public List<Product> GetProductByUserId(int userId)
        {
            var products = from product in _databaseContext.Products
                           from sale in _databaseContext.Sales
                           where product.ProductId == sale.ProductId && sale.UserId == userId
                           select product;

            return products.ToList();
        }

        public List<Product> GetProductByCatagoryName(string catagoryName)
        {
            var products = from product in _databaseContext.Products
                           where product.CatagoryName.Equals(catagoryName)
                           select product;

            return products.ToList();
        }

        public List<Product> GetProductBySubCatagoryName(string subCatagoryName)
        {
            var products = from product in _databaseContext.Products
                           where product.SubCatagoryName.Equals(subCatagoryName)
                           select product;
            return products.ToList();

        }

        public List<Product> GetProductBySubCatagoryName(string catagoryName, string subCatagoryName)
        {
            var products = from product in _databaseContext.Products
                           where product.CatagoryName.Equals(catagoryName) & product.SubCatagoryName.Equals(subCatagoryName)
                           select product;
            return products.ToList();

        }

        public List<Product> GetProducts(int index, int size)
        {
            return _databaseContext.Products.Select(product => product).OrderBy(p => p.ProductId).Skip(index * size).Take(size).ToList();
        }

    }
}