using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;
using CodeWarriors.IITDU.Repository;

namespace CodeWarriors.IITDU.Service
{
    public class RatingService
    {
        private readonly RatingRepository _ratingRepository;
        private Rating _rating;
        private readonly UserRepository _userRepository;
        private readonly ProductRepository _productRepository;
        private readonly SaleRepository _saleRepository;
        public RatingService(RatingRepository ratingRepository, ProductRepository productRepository, UserRepository userRepository, SaleRepository saleRepository, Rating rating)
        {
            _ratingRepository = ratingRepository;
            _rating = rating;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _saleRepository = saleRepository;
        }
        public double AddRating(int productId, int rating, string userName)
        {
            var product = _productRepository.GetProductByProductId(productId);
            if (_saleRepository.GetUserId(productId) != _userRepository.GetUserId(userName))
            {

                _rating.Rate = rating;
                _rating.ProductId = productId;
                _rating.UserId = _userRepository.GetUserId(userName);
                if (product.AverageRate == 0)
                    product.AverageRate = rating;
                else product.AverageRate = (product.AverageRate + rating) / 2;
                _productRepository.Update(product);
                _ratingRepository.Add(_rating);
            }
            return product.AverageRate;
        }

    }
}