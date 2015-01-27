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
            var userId = _userRepository.GetUserId(userName);
            if (_saleRepository.GetUserId(productId) != userId)
            {
                var existingRating = _ratingRepository.GetRatingByUserId(userId);
                if (existingRating != null)
                {
                    product.AverageRate = 2 * product.AverageRate - existingRating.Rate;
                    _ratingRepository.Remove(existingRating);
                }
                _rating.Rate = rating;
                _rating.ProductId = productId;
                _rating.UserId = userId;
                _ratingRepository.Add(_rating);
                product.AverageRate = _ratingRepository.GetAll().Average(e => e.Rate);
                _productRepository.Update(product);
                
            }
            return product.AverageRate;
        }

    }
}