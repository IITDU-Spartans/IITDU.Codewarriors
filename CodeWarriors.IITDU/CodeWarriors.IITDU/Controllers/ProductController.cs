using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeWarriors.IITDU.Models;
using CodeWarriors.IITDU.Service;
using CodeWarriors.IITDU.ViewModels;

namespace CodeWarriors.IITDU.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private Product _product;
        public ProductController(ProductService productService, Product product)
        {
            _productService = productService;
            _product = product;
        }

        [HttpPost]
        public ActionResult AddProduct(List<String> values)
        {
            var category = values[1];
            _product.ProductName = values[0];
            _product.CatagoryName = category;
            _product.SubCatagoryName = values[2];
            _product.Price = int.Parse(values[3]);
            _product.Price = int.Parse(values[4]);
            _product.Manufacturer = values[5];
            List<String> imageUrls = new List<string>();
            
            if (category == "Apparels")
            {
                _product.AvailableSizes = values[6];
                _product.Description = values[7];
                for (int i = 8; i < values.Count(); i++)
                {
                    imageUrls.Add(values[i]);
                }
            }
            else if (category == "Electronics"||category=="Accessories")
            {
                _product.AvailableModels = values[6];
                _product.Description = values[7];
                for (int i = 8; i < values.Count(); i++)
                {
                    imageUrls.Add(values[i]);
                }
            }
            else
            {
                _product.Description = values[6];
                for (int i = 7; i < values.Count(); i++)
                {
                    imageUrls.Add(values[i]);
                }
            }
            
            _product.ImageUrls = imageUrls;
            _productService.AddProduct(_product,User.Identity.Name);
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
        [AllowAnonymous]
        public ActionResult GetProducts(int index, int size)
        {
            var products = _productService.GetProducts(index, size);
            return Json(products, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllProductByUser()
        {
            var products = _productService.GetAllProductByUserName(User.Identity.Name);
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateProduct(List<String> values)
        {
            var category = values[2];
            _product.ProductId = int.Parse(values[0]);
            _product.ProductName = values[1];
            _product.CatagoryName = category;
            _product.SubCatagoryName = values[3];
            _product.Price = int.Parse(values[4]);
            _product.Price = int.Parse(values[5]);
            _product.Manufacturer = values[6];
            List<String> imageUrls = new List<string>();

            if (category == "Apparels")
            {
                _product.AvailableSizes = values[7];
                _product.Description = values[8];
                for (int i = 9; i < values.Count(); i++)
                {
                    imageUrls.Add(values[i]);
                }
            }
            else if (category == "Electronics" || category == "Accessories")
            {
                _product.AvailableModels = values[7];
                _product.Description = values[8];
                for (int i = 9; i < values.Count(); i++)
                {
                    imageUrls.Add(values[i]);
                }
            }
            else
            {
                _product.Description = values[7];
                for (int i = 8; i < values.Count(); i++)
                {
                    imageUrls.Add(values[i]);
                }
            }

            _product.ImageUrls = imageUrls;
            _productService.AddProduct(_product, User.Identity.Name);
            return Json("Product Added Successfully", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetFormElements(String category)
        {
            List<String> elementList = new List<string>()
            {
                "Product Name",
                "Category",
                "Subcategory",
                "Price",
                "Available Count",
                "Manufacturer"
            };
            if (category == "Apparels")
            {
                elementList.Add("Available Sizes");
            }
            else if (category == "Electronics")
            {
                elementList.Add("Available Models");
                
            }
            else if(category=="Accessories")
            {
                elementList.Add("Available Models");
            }
            elementList.Add("Description");
            return Json(elementList, JsonRequestBehavior.AllowGet);
        }

    }
}
