using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeWarriors.IITDU.Service;

namespace CodeWarriors.IITDU.Controllers
{
    [Authorize]
    public class RatingController : Controller
    {
        private readonly RatingService _ratingService;
        
        public RatingController(RatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        public ActionResult AddRating(int sellerId, int rating)
        {
            var average = _ratingService.AddRating(sellerId, rating, User.Identity.Name);
            return Json(average,JsonRequestBehavior.AllowGet);
        }



    }
}
