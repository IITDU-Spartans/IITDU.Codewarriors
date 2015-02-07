using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeWarriors.IITDU.Service;

namespace CodeWarriors.IITDU.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpPost]
        public ActionResult AddReview(int productId, string review)
        {
            _reviewService.AddReview(productId, review, User.Identity.Name);
            return Json("Review Added Successfuly", JsonRequestBehavior.AllowGet);
        }

        //public ActionResult AddReviews()
        //{
        //    return Json(null);
        //}

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetReviews(int productId)
        {
            return Json(_reviewService.GetReviewsByProductId(productId), JsonRequestBehavior.AllowGet);
        }

    }
}
