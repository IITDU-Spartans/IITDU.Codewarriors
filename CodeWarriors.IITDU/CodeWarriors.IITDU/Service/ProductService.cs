using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;
using CodeWarriors.IITDU.Repository;
using CodeWarriors.IITDU.ViewModels;

namespace CodeWarriors.IITDU.Service
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly SaleRepository _saleRepository;
        private readonly CategoryRepository _catagoryRepository;
        private Product _product;
        private Sale _sale;
        private Category _category;
        private readonly UserRepository _userRepository;
        public ProductService(UserRepository userRepository, ProductRepository productRepository, SaleRepository saleRepository, CategoryRepository catagoryRepository, Product product, Sale sale, Category category)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _saleRepository = saleRepository;
            _catagoryRepository = catagoryRepository;
            _product = product;
            _sale = sale;
            _category = category;
        }
        public bool AddProduct(ProductViewModel model, string userName)
        {
            //product
            _product.ProductName = model.ProductName;
            _product.Price = model.Price;
            _product.Description = model.Description;
            _product.ImageUrl = model.ImageUrl;
            _product.AvailableCount = model.AvailableCount;
            int categoryId = _catagoryRepository.GetCategoryIdByCategoryName(model.CategoryName);

            if (categoryId == 0)
            {
                //category
                _category.Name = model.CategoryName;
                _catagoryRepository.Add(_category);
                categoryId = _catagoryRepository.GetCategoryIdByCategoryName(model.CategoryName);
            }
            _product.CatagoryId = categoryId;

            //sale
            _sale.ProductId = _productRepository.AddProduct(_product);
            _sale.UserId = _userRepository.GetUserId(userName);
            _sale.UploadDateTime = DateTime.Now;

            return _saleRepository.Add(_sale);
        }

        public bool RemoveProduct(int productId)
        {
            var product = _productRepository.Get(productId);
            var sale = _saleRepository.GetSaleByProductId(productId);
            return _productRepository.Remove(product) && _saleRepository.Remove(sale);
        }

        public List<Product> GetAllProduct()
        {
            return _productRepository.GetAll();
        }
        public List<Product> GetAllProductByUserName(string userName)
        {
            return _productRepository.GetProductByUserId(_userRepository.GetUserId(userName));
        }

        public Product GetProduct(int productId)
        {
            return _productRepository.Get(productId);
        }

        public bool UpdateProduct(ProductViewModel model, int productId)
        {
            //product
            _product.ProductId = productId;
            _product.ProductName = model.ProductName;
            _product.Price = model.Price;
            _product.Description = model.Description;
            _product.ImageUrl = model.ImageUrl;
            _product.AvailableCount = model.AvailableCount;
            int categoryId = _catagoryRepository.GetCategoryIdByCategoryName(model.CategoryName);

            if (categoryId == 0)
            {
                //category
                _category.Name = model.CategoryName;
                _catagoryRepository.Add(_category);
                categoryId = _catagoryRepository.GetCategoryIdByCategoryName(model.CategoryName);
            }
            _product.CatagoryId = categoryId;
            return _productRepository.Update(_product);
        }

        public String GetProductCategory(int categoryId)
        {
            return  _catagoryRepository.Get(categoryId).Name;
        }

        public List<Product> GetAllProductsByProductId(List<int> idList)
        {
            var products = from product in GetAllProduct()
                join id in idList on product.ProductId equals id
                select product;
            return products.ToList();
        }

        public int GetUserIdByProductId(int productId)
        {
            return _saleRepository.GetUserId(productId);
        }
    }
}