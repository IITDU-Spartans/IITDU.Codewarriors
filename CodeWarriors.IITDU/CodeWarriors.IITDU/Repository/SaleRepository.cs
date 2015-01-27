using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CodeWarriors.IITDU.Models;
using Ninject.Web.Common;

namespace CodeWarriors.IITDU.Repository
{
    public class SaleRepository:IRepository<Sale>
    {
        private DatabaseContext _databaseContext;
        public SaleRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Add(Sale model)
        {
            _databaseContext.Sales.Add(model);
            _databaseContext.SaveChanges();
            return true;
            
        }

        public bool Remove(Sale model)
        {
            if (!IsSaleExists(model.SaleId))
                return false;
            _databaseContext.Sales.Remove(model);
            _databaseContext.SaveChanges();
            return true;
        }

        public bool Update(Sale model)
        {
            if (!IsSaleExists(model.SaleId))
                return false;
            _databaseContext.Entry(model).State=EntityState.Modified;
            _databaseContext.SaveChanges();
            return true;
        }

        public List<Sale> GetAll()
        {
            var query = from sale in _databaseContext.Sales select sale;
            return query.ToList();
        }

        public Sale Get(int modelId)
        {
            if (!IsSaleExists(modelId))
                return new NullSale();
            var query = from sale in _databaseContext.Sales where sale.SaleId.Equals(modelId) select sale;
            return query.First();

        }
        private bool IsSaleExists(int saleId)
        {
            return _databaseContext.Sales.Any(sale => sale.SaleId.Equals(saleId));
        }

        public Sale GetSaleByProductId(int productId)
        {
            return _databaseContext.Sales.FirstOrDefault(sale => sale.ProductId == productId);
        }

        public int GetUserId(int productId)
        {
            return _databaseContext.Sales.Where(s => s.ProductId == productId).Select(s => s.UserId).First();
        }

    }
}