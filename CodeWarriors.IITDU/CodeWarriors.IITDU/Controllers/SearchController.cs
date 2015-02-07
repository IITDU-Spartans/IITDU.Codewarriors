using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeWarriors.IITDU.Repository;
using CodeWarriors.IITDU.Service;

namespace CodeWarriors.IITDU.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        private ProductService _productService;
        private SearchService _searchService;

        public SearchController(ProductService productService, SearchService searchService)
        {
            _productService = productService;
            _searchService = searchService;
        }

        [HttpGet]
        public JsonResult Search(string query, int index, int size)
        {
            var products = _searchService.SearchProducts(query, index, size);
            List<List<string>> images = products.Select(product => _productService.GetProductImages(product.ProductId)).ToList();
            return Json(new { products, images }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public void CreateInitialIndex()
        {
            _searchService.CreateIndex();
        }
    }
}
