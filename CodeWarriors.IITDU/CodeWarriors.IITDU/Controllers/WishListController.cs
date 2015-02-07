using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeWarriors.IITDU.Service;

namespace CodeWarriors.IITDU.Controllers
{
    [Authorize]
    public class WishListController : Controller
    {
        private readonly WishListService _wishListService;
        private readonly ProductService _productService;
        public WishListController(WishListService wishListService, ProductService productService)
        {
            _wishListService = wishListService;
            _productService = productService;
        }
        [HttpPost]
        public ActionResult AddToWishList(int productId)
        {
            var message = _wishListService.AddWishedProduct(productId, User.Identity.Name);
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RemoveFromWishList(int wishId)
        {
            _wishListService.RemoveWishedProduct(wishId);
            return Json("Product removed from Wishlist successfully", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetWishList()
        {
            var wishList = _wishListService.GetWishList(User.Identity.Name);
            var ids = from item in wishList select item.ProductId;
            var products = _productService.GetAllProductsByProductId(ids.ToList());
            var wishItems = from wishedItem in wishList
                            from product in products where wishedItem.ProductId.Equals(product.ProductId)
                            select new { product = product, wishId = wishedItem.WishedProductId };
            //new { wishedItem.ProductId, wishedItem.WishedProductId, product.ProductName, product.Price }
            List<List<string>> images = wishItems.Select(wishedItem => _productService.GetProductImages(wishedItem.product.ProductId)).ToList();
            return Json(new { wishItems, images }, JsonRequestBehavior.AllowGet); ;
            //return Json(wishItems, JsonRequestBehavior.AllowGet);
        }

    }
}
