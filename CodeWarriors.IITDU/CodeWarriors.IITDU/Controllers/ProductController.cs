﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CodeWarriors.IITDU.Models;
using CodeWarriors.IITDU.Repository;
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
            _product.AvailableCount = int.Parse(values[4]);
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
            else if (category == "Electronics" || category == "Accessories")
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

            var Id = _productService.AddProduct(_product, User.Identity.Name);
            _productService.SaveProductImages(imageUrls, Id);
            return Json(new { Message = "Product Added Successfully", ProductId = Id }, JsonRequestBehavior.AllowGet);
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
            product.AverageRate++;
            _productService.UpdateProduct(product);
            var owner = false;
            if (!string.IsNullOrEmpty(User.Identity.Name))
                owner = _productService.IsOwnProduct(productId, User.Identity.Name);
            var images = _productService.GetProductImages(productId);
            return Json(new
            {
                product,
                CategoryName = _productService.GetProductCategory(product.CatagoryId),
                IsOwner = owner,
                images
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
            List<List<string>> images = products.Select(product => _productService.GetProductImages(product.ProductId)).ToList();
            return Json(new { products, images }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllProductByUser()
        {
            var products = _productService.GetAllProductByUserName(User.Identity.Name);
            List<List<string>> images = products.Select(product => _productService.GetProductImages(product.ProductId)).ToList();
            return Json(new { products, images }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product product)
        {
            _productService.UpdateProduct(product);
            return Json("Product Updated Successfully", JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult GetProductByCategory(String category, int index, int size)
        {
            var products = _productService.GetProductByCatagoryName(category, index, size);
            List<List<string>> images = products.Select(product => _productService.GetProductImages(product.ProductId)).ToList();
            return Json(new { products, images }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult GetProductBySubCategory(String category, String subCategory, int index, int size)
        {
            var products = _productService.GetProductBySubCatagoryName(category, subCategory, index, size);
            List<List<string>> images = products.Select(product => _productService.GetProductImages(product.ProductId)).ToList();
            return Json(new { products, images }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
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
            else if (category == "Accessories")
            {
                elementList.Add("Available Models");
            }
            elementList.Add("Description");
            return Json(elementList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetPurchasedProducts()
        {
            var products = _productService.GetPurchasedProducts(User.Identity.Name);
            List<List<string>> images = products.Select(product => _productService.GetProductImages(product.ProductId)).ToList();
            return Json(new { products, images }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSoldProducts()
        {
            var products = _productService.GetSoldProducts(User.Identity.Name);
            List<List<string>> images = products.Select(product => _productService.GetProductImages(product.ProductId)).ToList();
            return Json(new { products, images }, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult GetRecommendedProducts()
        {
            UserRepository userRepository = new UserRepository(new DatabaseContext());
            var products = _productService.GetRecommendedProducts(userRepository.GetUserId(User.Identity.Name));
            List<List<string>> images = products.Select(product => _productService.GetProductImages(product.ProductId)).ToList();
            return Json(new { products, images }, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult GetRelevantProducts(int productId)
        {
            Product currentProduct = _productService.GetProduct(productId);
            var products = _productService.GetProductBySubCatagoryName(currentProduct.CatagoryName, currentProduct.SubCatagoryName, 0, 100).ToList();
            products.Remove(currentProduct);
            List<List<string>> images = products.Select(product => _productService.GetProductImages(product.ProductId)).ToList();
            return Json(new { products, images }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductGallaryImages()
        {
            var products = _productService.GetAllProduct();
            List<List<string>> images = products.Select(product => _productService.GetProductImages(product.ProductId)).ToList();
            return Json(images, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult GetBuyerId(int orderId)
        {
            return Json(_productService.GetBuyerId(orderId), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult GetGroupedProductsByBuyers()
        {
            UserRepository userRepository = new UserRepository(new DatabaseContext());
            ProductOrderService productOrderService = new ProductOrderService();
            List<BuyerWithProducts> productsGroupByBuyers = productOrderService.GetProductsWithBuyers(userRepository.GetUserId(User.Identity.Name));
            return Json(productsGroupByBuyers, JsonRequestBehavior.AllowGet);
        }
    }
}
