using CodeWarriors.IITDU.Models;
using CodeWarriors.IITDU.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeWarriors.IITDU.Service
{
    public class ProductOrderService
    {
        private ProductService _productService = new ProductService(new UserRepository(new DatabaseContext()), new ProductRepository(new DatabaseContext()), new SaleRepository(new DatabaseContext()), new CategoryRepository(new DatabaseContext()), new Product(), new Sale(), new Category(), new PurchaseRepository(new DatabaseContext()), new ProductImageRepository(new DatabaseContext()), new Purchase(), new ProductImage(), new SearchService(new SearchRepository()));
        public List<BuyerWithProducts> GetProductsWithBuyers(int sellerId)
        {
            ProductOrderRepository productOrderRepository = new ProductOrderRepository(new DatabaseContext());
            ProfileRepository profileRepository = new ProfileRepository(new DatabaseContext());
            ProductRepository productRepository = new ProductRepository(new DatabaseContext());

            List<int> buyerIds = productOrderRepository.GetBuyerIdBySellerId(sellerId).Distinct().ToList();

            List<BuyerWithProducts> list = new List<BuyerWithProducts>();

            foreach (var buyerId in buyerIds)
            {
                BuyerWithProducts buyerWithProducts = new BuyerWithProducts();
                buyerWithProducts.FirstName = profileRepository.Get(buyerId).FirstName;
                buyerWithProducts.UserId = buyerId;

                var productIds = productOrderRepository.GetProductIdFromBuyerId(buyerId);
                foreach (var productId in productIds)
                {
                    buyerWithProducts.Products.Add(productRepository.Get(productId));
                }
                buyerWithProducts.Images = buyerWithProducts.Products.Select(product => _productService.GetProductImages(product.ProductId)).ToList();
                list.Add(buyerWithProducts);
            }

            return list;

        }
    }

    public class BuyerWithProducts
    {
        public int UserId { get; set; }
        public String FirstName { get; set; }
        public List<Product> Products { get; set; }
        public List<List<String>> Images { get; set; }
        public BuyerWithProducts()
        {
            Products=new List<Product>();
            

        }
    }
}