using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;

namespace CodeWarriors.IITDU.Service
{
    public interface ISearchService
    {
        void CreateIndex();
        IEnumerable<Product> SearchProducts(string query, int index, int size);
        void AddNewProductInIndex(Product product);
        void DeleteProductFromIndex(int productId);
    }
}