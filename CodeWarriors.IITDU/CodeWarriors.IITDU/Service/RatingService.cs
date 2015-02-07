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
        private ProfileRepository _profileRepository;
        public RatingService(RatingRepository ratingRepository, UserRepository userRepository, Rating rating, ProfileRepository profileRepository)
        {
            _ratingRepository = ratingRepository;
            _rating = rating;
            _userRepository = userRepository;
            _profileRepository = profileRepository;
        }
        public double AddRating(int sellerId, int rating, string userName)
        {
            var seller = _profileRepository.Get(sellerId);
            _rating.BuyerId = _userRepository.GetUserId(userName);
            _rating.SellerId = sellerId;
            _rating.Rate = rating;
            _ratingRepository.Add(_rating);
            seller.AverageRating = _ratingRepository.GetAll().Average(e=>e.Rate);
            _profileRepository.Update(seller, sellerId);
            return seller.AverageRating;
        }

    }
}