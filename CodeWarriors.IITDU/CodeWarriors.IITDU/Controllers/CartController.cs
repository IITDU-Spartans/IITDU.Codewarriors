using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeWarriors.IITDU.Models;
using CodeWarriors.IITDU.Service;

namespace CodeWarriors.IITDU.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ProductService _productService;
        private readonly CartService _cartService;
        public CartController(ProductService productService, CartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        [HttpPost]
        public ActionResult AddToCart(CartItem cartItem)
        {
            List<CartItem> cartList = (List<CartItem>)Session["CartList"];
            var item = cartList.FirstOrDefault(p => p.ProductId == cartItem.ProductId);
            var message = "Product is already in the cart";
            if (item == null && !_cartService.IsOwnProduct(cartItem.ProductId, User.Identity.Name))
            {
                var product = _productService.GetProduct(cartItem.ProductId);
                if (product.AvailableCount > 0)
                {
                    cartItem.AddTime = DateTime.Now;
                    cartItem.ProductName = product.ProductName;
                    cartItem.Price = product.Price;
                    cartList.Add(cartItem);
                    message = "Product added to cart successfully";
                }
                else
                {
                    message = "Product is not available";
                }
            }

            Session["CartList"] = cartList;
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RemoveFromCart(int productId)
        {
            List<CartItem> cartList = (List<CartItem>)Session["CartList"];
            var item = cartList.FirstOrDefault(p => p.ProductId == productId);
            cartList.Remove(item);
            return Json("Product removed from cart successfully", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllCartItems()
        {
            List<CartItem> cartList = (List<CartItem>)Session["CartList"];

            return Json(cartList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CheckOut()
        {
            List<CartItem> cartList = (List<CartItem>)Session["CartList"];
            var idList = cartList.Select(c => c.ProductId).ToList();
            _productService.Purchase(idList, User.Identity.Name);
            cartList.Clear();
            Session["CartList"] = cartList;
            return Json("Your request for check out is successful. Thank you.", JsonRequestBehavior.AllowGet);
        }
    }
}
