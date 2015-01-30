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
        private readonly PurchaseRepository _purchaseRepository;
        private ProductImageRepository _productImageRepository;
        private Purchase _purchase;
        private ProductImage _productImage;
        public ProductService(UserRepository userRepository, ProductRepository productRepository, SaleRepository saleRepository,
            CategoryRepository catagoryRepository, Product product, Sale sale, Category category,
            PurchaseRepository purchaseRepository, ProductImageRepository productImageRepository, Purchase purchase,
            ProductImage productImage)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _saleRepository = saleRepository;
            _catagoryRepository = catagoryRepository;
            _purchaseRepository = purchaseRepository;
            _product = product;
            _sale = sale;
            _category = category;
            _purchase = purchase;
            _productImageRepository = productImageRepository;
            _productImage = productImage;
        }
        public int AddProduct(Product product, string userName)
        {
            //_productRepository.Add(product);
            //sale
            _sale.ProductId = _productRepository.AddProduct(product);
            _sale.UserId = _userRepository.GetUserId(userName);
            _sale.UploadDateTime = DateTime.Now;
            _saleRepository.Add(_sale);
            //NotifyUsers(product);
            return _sale.ProductId;
        }
        public List<Product> GetProducts(int index, int size)
        {
            return _productRepository.GetProducts(index, size);
        }
        public bool RemoveProduct(int productId)
        {
            var product = _productRepository.Get(productId);
            var sale = _saleRepository.GetSaleByProductId(productId);
            _productImageRepository.RemoveAllImagesByProductId(productId);
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

        public bool UpdateProduct(Product product)
        {
            return _productRepository.Update(_product);
        }

        public String GetProductCategory(int categoryId)
        {
            return _catagoryRepository.Get(categoryId).Name;
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

        public void Purchase(List<int> productIdList, string userName)
        {
            var userId = _userRepository.GetUserId(userName);
            foreach (var id in productIdList)
            {
                var product = _productRepository.Get(id);
                product.PurchaseCount++;
                product.AvailableCount--;
                _productRepository.Update(product);
                _purchase.ProductId = id;
                _purchase.PurchaseTime = DateTime.Now;
                _purchase.UserId = userId;
                _purchaseRepository.Add(_purchase);
            }
        }

        public bool IsOwnProduct(int productId, string userName)
        {
            return _saleRepository.GetUserId(productId) == _userRepository.GetUserId(userName);
        }


        public void SaveProductImages(List<String> images, int productId)
        {
            foreach (var image in images)
            {
                _productImage.ProductId = productId;
                _productImage.ImageUrl = image;
                _productImageRepository.Add(_productImage);
            }
        }
        public List<Product> GetProductByCatagoryName(string catagoryName, int index, int size)
        {
            return _productRepository.GetProductByCatagoryName(catagoryName, index, size);
        }

        public List<Product> GetProductBySubCatagoryName(string catagoryName, string subCatagoryName, int index, int size)
        {
            return _productRepository.GetProductBySubCatagoryName(catagoryName, subCatagoryName, index, size);
        }
        public List<String> GetProductImages(int productId)
        {
            return _productImageRepository.GetImagesByProductId(productId).Select(e => e.ImageUrl).ToList();
        }

        private void NotifyUsers(Product product)
        {
            MailService mailService = new MailService();
            WishedProductRepository wishedProductRepository = new WishedProductRepository(new DatabaseContext());
            List<User> users = wishedProductRepository.GetAllUser(product.SubCatagoryName);
            mailService.SendMail(users, product);
        }
    }
}