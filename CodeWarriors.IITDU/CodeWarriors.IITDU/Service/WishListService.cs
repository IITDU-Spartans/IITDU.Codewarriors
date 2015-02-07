using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;
using CodeWarriors.IITDU.Repository;

namespace CodeWarriors.IITDU.Service
{
    public class WishListService
    {
        private readonly WishedProductRepository _wishedProductRepository;
        private readonly ProductRepository _productRepository;
        private readonly UserRepository _userRepository;
        private readonly SaleRepository _saleRepository;
        private WishedProduct _wishedProduct;
        public WishListService(WishedProductRepository wishedProductRepository, ProductRepository productRepository,
            UserRepository userRepository, SaleRepository saleRepository, WishedProduct wishedProduct)
        {
            _wishedProductRepository = wishedProductRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _saleRepository = saleRepository;
            _wishedProduct = wishedProduct;
        }

        public string AddWishedProduct(int productId, string userName)
        {
            var userId = _userRepository.GetUserId(userName);
            var wishList = _wishedProductRepository.GetAllByUserId(userId);
            if (_saleRepository.GetUserId(productId) != userId && wishList.All(e => e.ProductId != productId))
            {
                _wishedProduct.ProductId = productId;
                _wishedProduct.UserId = userId;
                var product = _productRepository.Get(productId);
                product.WishCount++;
                _wishedProductRepository.Add(_wishedProduct);
                _productRepository.Update(product);
                return "Product added to Wishlist successfully";
            }
            return "Product could not be added to Wishlist";
        }

        public List<WishedProduct> GetWishList(string userName)
        {
            int userId = _userRepository.GetUserId(userName);
            return _wishedProductRepository.GetAllByUserId(userId);
        }

        public void RemoveWishedProduct(int wishId)
        {
            var product = _productRepository.GetProductByProductId(_wishedProductRepository.Get(wishId).ProductId);
            product.WishCount--;
            _productRepository.Update(product);
            _wishedProductRepository.RemoveById(wishId);
        }
    }
}