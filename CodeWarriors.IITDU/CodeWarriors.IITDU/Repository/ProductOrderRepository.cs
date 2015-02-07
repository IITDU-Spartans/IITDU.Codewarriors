using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;

namespace CodeWarriors.IITDU.Repository
{
    public class ProductOrderRepository:IRepository<ProductOrder>
    {
        private DatabaseContext _databaseContext;
        public ProductOrderRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public bool Add(ProductOrder model)
        {
            _databaseContext.ProductOrders.Add(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Remove(ProductOrder model)
        {
            _databaseContext.ProductOrders.Remove(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Update(ProductOrder model)
        {
            _databaseContext.Entry(model).State=EntityState.Modified;
            return _databaseContext.SaveChanges() > 0;
        }

        public List<ProductOrder> GetAll()
        {
            return _databaseContext.ProductOrders.ToList();
        }

        public ProductOrder Get(int id)
        {
            return _databaseContext.ProductOrders.Find(id);
        }

        public bool RemoveOrderById(int id)
        {
            var order = Get(id);
            return Remove(order);
        }


        public List<int> GetBuyerIdBySellerId(int sellerId)
        {
            var sql = from productOrder in _databaseContext.ProductOrders
                      where productOrder.SellerId.Equals(sellerId)
                      select productOrder.BuyerId;
            return sql.ToList();
        }

        public List<int> GetProductIdFromBuyerId(int buyerId)
        {
            var sql = from productOrder in _databaseContext.ProductOrders
                      where productOrder.BuyerId.Equals(buyerId) & productOrder.DeliveryStatus.Equals(false)
                      select productOrder.ProductId;
            return sql.ToList();
        }
    }
}