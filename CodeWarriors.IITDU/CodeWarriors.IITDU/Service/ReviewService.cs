using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;
using CodeWarriors.IITDU.Repository;

namespace CodeWarriors.IITDU.Service
{
    public class ReviewService
    {
        private ReviewRepository _reviewRepository;
        private ProductRepository _productRepository;
        private Review _review;
        private UserRepository _userRepository;
        public ReviewService(ReviewRepository reviewRepository, ProductRepository productRepository, Review review, 
            UserRepository userRepository)
        {
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
            _review = review;
            _userRepository = userRepository;
        }

        public void AddReview(int pid, string reviewDesc, string email)
        {
            _review.ProductId = pid;
            _review.ReviewDescription = reviewDesc;
            _review.ReviewDateTime = DateTime.Now;
            _review.UserId = _userRepository.GetUserId(email);
            _reviewRepository.Add(_review);
        }

        public List<CommentModel> GetReviewsByProductId(int pid)
        {
            var list = _reviewRepository.GetReviewsByProductId(pid);
            var reviews = new List<CommentModel>();
            foreach (var item in list)
            {
                reviews.Add(new CommentModel()
                {
                    ReviewDescription = item.ReviewDescription,
                    Time = item.ReviewDateTime,
                    UserName = _userRepository.Get(item.UserId).Email
                });
            }
            return reviews;
        } 
    }
}