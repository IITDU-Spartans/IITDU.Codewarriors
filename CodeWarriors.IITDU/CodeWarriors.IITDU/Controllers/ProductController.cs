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
        private ProductViewModel _productViewModel;
        public ProductController(ProductService productService, ProductViewModel productViewModel)
        {
            _productService = productService;
            _productViewModel = productViewModel;
        }

        [HttpPost]
        public ActionResult AddProduct(ProductViewModel model)
        {
            model.ImageUrl = "Upload/" + model.ImageUrl;
            _productService.AddProduct(model, User.Identity.Name);
            return Json("Product Added Successfully", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveProduct(int productId)
        {
            _productService.RemoveProduct(productId);
            return Json("Product Removed Successfully", JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult GetProduct(int productId)
        {
            var product = _productService.GetProduct(productId);
            var owner = false;
            if (!string.IsNullOrEmpty(User.Identity.Name))
                owner = _productService.IsOwnProduct(productId, User.Identity.Name);
            return Json(new
            {
                product,
                CategoryName = _productService.GetProductCategory(product.CatagoryId),
                IsOwner = owner
            }, JsonRequestBehavior.AllowGet);
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
            model.ImageUrl = "Upload/" + model.ImageUrl;
            _productService.UpdateProduct(model, productId);
            return Json("Product updated successfully");
        }
    }
}
