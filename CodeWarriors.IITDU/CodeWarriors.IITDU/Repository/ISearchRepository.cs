using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;
using Lucene.Net.Documents;

namespace CodeWarriors.IITDU.Repository
{
    public interface ISearchRepository
    {
        void CreateIndex(IEnumerable<Document> documents);
        Document CreateProductDocument(Product product);
        void AddDocument(Document document);
        void AddProductToDocument(Product product);
        IEnumerable<string> Search(string queryString);
        void DeleteDocument(string productId);

    }
}