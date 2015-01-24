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
        private readonly CatagoryRepository _catagoryRepository;
        private Product _product;
        private Sale _sale;
        private Catagory _catagory;
        private readonly UserRepository _userRepository;
        public ProductService(UserRepository userRepository, ProductRepository productRepository, SaleRepository saleRepository, CatagoryRepository catagoryRepository, Product product, Sale sale, Catagory catagory)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _saleRepository = saleRepository;
            _catagoryRepository = catagoryRepository;
            _product = product;
            _sale = sale;
            _catagory = catagory;
        }
        public bool AddProduct(ProductViewModel model, string userName)
        {
            //product
            _product.ProductName = model.ProductName;
            _product.Price = model.Price;
            _product.Description = model.Description;
            _product.ImageUrl = model.ImageUrl;
            _product.AvailableCount = model.AvailableCount;
            int categoryId = _catagoryRepository.GetCategoryIdByCategoryName(model.CatagoryName);

            if (categoryId == 0)
            {
                //category
                _catagory.Name = model.CatagoryName;
                _catagoryRepository.Add(_catagory);
                categoryId = _catagoryRepository.GetCategoryIdByCategoryName(model.CatagoryName);
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
            int categoryId = _catagoryRepository.GetCategoryIdByCategoryName(model.CatagoryName);

            if (categoryId == 0)
            {
                //category
                _catagory.Name = model.CatagoryName;
                _catagoryRepository.Add(_catagory);
                categoryId = _catagoryRepository.GetCategoryIdByCategoryName(model.CatagoryName);
            }
            _product.CatagoryId = categoryId;
            return _productRepository.Update(_product);
        }


    }
}