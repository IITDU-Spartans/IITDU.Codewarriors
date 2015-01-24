using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeWarriors.IITDU.Service;
using CodeWarriors.IITDU.ViewModels;

namespace CodeWarriors.IITDU.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public ActionResult AddProduct(ProductViewModel model)
        {
            _productService.AddProduct(model, User.Identity.Name);
            return Json("Product Added Successfully", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveProduct(int productId)
        {
            _productService.RemoveProduct(productId);
            return Json("Product Removed Successfully", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProduct(int productId)
        {
            return Json(_productService.GetProduct(productId), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult GetAllProducts()
        {
            var products = _productService.GetAllProduct();
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllProductByUser()
        {
            var products = _productService.GetAllProductByUserName(User.Identity.Name);
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateProduct(ProductViewModel model, int productId)
        {
            _productService.UpdateProduct(model, productId);
            return Json("Product updated successfully");
        }
    }
}
