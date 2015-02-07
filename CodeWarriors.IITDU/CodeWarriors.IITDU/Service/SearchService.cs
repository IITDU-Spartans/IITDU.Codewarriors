using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;
using CodeWarriors.IITDU.Repository;
using Lucene.Net.Documents;

namespace CodeWarriors.IITDU.Service
{
    public class SearchService : ISearchService
    {
        private SearchRepository _searchRepository;
        private DatabaseContext _databaseContext = new DatabaseContext();

        public SearchService(SearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }
        public void CreateIndex()
        {
            var documents = new List<Document>();
            var products = _databaseContext.Products.ToList();
            foreach (var product in products)
            {
                documents.Add(_searchRepository.CreateProductDocument(product));
            }
            _searchRepository.CreateIndex(documents);
        }

        public IEnumerable<Product> SearchProducts(string query, int index, int size)
        {
            var productIds = _searchRepository.Search(query);
            var searchResults = new List<Product>();

            foreach (var productId in productIds)
            {
                searchResults.Add(_databaseContext.Products.Find(Convert.ToInt32(productId)));
            }
            return searchResults.OrderBy(p => p.ProductId).Skip(index * size).Take(size);
        }

        public void AddNewProductInIndex(Product product)
        {
            _searchRepository.AddProductToDocument(product);
        }

        public void DeleteProductFromIndex(int productId)
        {
            _searchRepository.DeleteDocument(productId.ToString());
        }
    }
}