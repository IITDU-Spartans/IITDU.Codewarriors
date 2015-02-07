using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;
using CodeWarriors.IITDU.Repository;
using CodeWarriors.IITDU.ViewModels;

namespace CodeWarriors.IITDU.Service
{
    public class CartService
    {
        private readonly UserRepository _userRepository;
        private readonly ProductRepository _productRepository;
        private readonly SaleRepository _saleRepository;
        private ProductOrderRepository _productOrderRepository;
        private ProductOrder _productOrder;
        private PurchaseRepository _purchaseRepository;
        private Purchase _purchase;
        private CartModel _cartModel;
        public CartService(UserRepository userRepository, ProductRepository productRepository, SaleRepository saleRepository,
            ProductOrderRepository productOrderRepository, ProductOrder productOrder,
            PurchaseRepository purchaseRepository, Purchase purchase, CartModel model)
        {
            _userRepository = userRepository;
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _productOrderRepository = productOrderRepository;
            _productOrder = productOrder;
            _purchase = purchase;
            _purchaseRepository = purchaseRepository;
            _cartModel = model;
        }

        public bool IsOwnProduct(int productId, string userName)
        {
            int userId = _userRepository.GetUserId(userName);
            return _saleRepository.GetUserId(productId) == userId;
        }

        public bool SaveCheckOutOrder(List<int> productIdList, string userEmail)
        {
            var userId = _userRepository.GetUserId(userEmail);
            foreach (int id in productIdList)
            {
                _productOrder.ProductId = id;
                _productOrder.BuyerId = userId;
                _productOrder.SellerId = _saleRepository.GetUserId(id);
                _productOrder.Price = (int)_productRepository.Get(id).Price;
                _productOrder.DeliveryStatus = false;
                _productOrderRepository.Add(_productOrder);
            }
            return true;
        }

        public bool ChangeStatus(int orderId, bool status)
        {
            var order = _productOrderRepository.Get(orderId);
            if (status)
            {
                _purchase.PurchaseTime = DateTime.Now;
                _purchase.ProductId = order.ProductId;
                _purchase.UserId = order.BuyerId;
                _purchaseRepository.Add(_purchase);

            }

            _productOrderRepository.Remove(order);

            return true;
        }

        public List<CartModel> GetCartListBySeller(List<int> productIds)
        {
            List<CartModel> models = new List<CartModel>();
            var sales = from sale in _saleRepository.GetAll()
            join id in productIds on sale.ProductId equals id
            select new {sale.UserId, sale.ProductId};
            var groupedSales = sales.GroupBy(s => s.UserId);
            foreach (var groupSale in groupedSales)
            {
                _cartModel.ProductIds = groupSale.Select(e=>e.ProductId).ToList();
                _cartModel.SellerId = groupSale.Key;
                var names = new List<String>();
                foreach (var id in _cartModel.ProductIds)
                {
                    var product = _productRepository.Get(id);
                    names.Add(product.ProductName);
                }
                _cartModel.ProductNames = names;
                _cartModel.SellerName = _userRepository.Get(_cartModel.SellerId).Email;
                models.Add(_cartModel);
            }
            return models;
        }

        public List<Product> GetAwaitingResponseProducts(string email)
        {
            int userId = _userRepository.GetUserId(email);
            var idList = _productOrderRepository.GetAll().Where(e => e.SellerId == userId).Select(e => e.ProductId).Distinct();
            List<Product> products = new List<Product>();
            foreach (var id in idList)
            {
                products.Add(_productRepository.Get(id));
            }
            return products;
        }

        public List<int> GetOrderIdList(string email)
        {
            int userId = _userRepository.GetUserId(email);
            return _productOrderRepository.GetAll().Where(e => e.SellerId == userId).Select(e => e.OrderId).Distinct().ToList();
        }
    }
}