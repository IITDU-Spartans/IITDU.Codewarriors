using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Repository;

namespace CodeWarriors.IITDU.Service
{
    public class CartService
    {
        private readonly UserRepository _userRepository;
        private readonly ProductRepository _productRepository;
        private readonly SaleRepository _saleRepository;
        public CartService(UserRepository userRepository, ProductRepository productRepository, SaleRepository saleRepository)
        {
            _userRepository = userRepository;
            _saleRepository = saleRepository;
            _productRepository = productRepository;
        }

        public bool IsOwnProduct(int productId, string userName)
        {
            int userId = _userRepository.GetUserId(userName);
            return _saleRepository.GetUserId(productId) == userId;
        }
    }
}