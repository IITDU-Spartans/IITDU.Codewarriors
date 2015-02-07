using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;
using Lucene.Net.Documents;
using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Version = Lucene.Net.Util.Version;

namespace CodeWarriors.IITDU.Repository
{
    public class SearchRepository : ISearchRepository
    {
        private string searchIndexDirectory = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "lucene_index");
        private readonly DirectoryInfo _directoryInfo;
        private Analyzer _analyzer;
        private IndexWriter _indexWriter;
        private FSDirectory _directory;


        private readonly string[] productSearchFields = { "Name", "Description", "Category", "SubCategory", "Manufacturer" };
        public SearchRepository()
        {
            _directoryInfo = new DirectoryInfo(searchIndexDirectory);
            _analyzer = new StandardAnalyzer(Version.LUCENE_30);
        }

        public void CreateIndex(IEnumerable<Document> documents)
        {
            using (_directory = FSDirectory.Open(_directoryInfo))
            {
                using (_indexWriter = new IndexWriter(_directory, _analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    foreach (var document in documents)
                    {
                        _indexWriter.AddDocument(document);
                    }
                    _indexWriter.Optimize();
                    _indexWriter.Flush(true, true, true);
                }

            }
        }

        public Document CreateProductDocument(Product product)
        {
            var document = new Document();

            document.Add(new Field("Id", product.ProductId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field(productSearchFields[0], product.ProductName, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field(productSearchFields[1], product.Description, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field(productSearchFields[2], product.CatagoryName, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field(productSearchFields[3], product.SubCatagoryName, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field(productSearchFields[4], product.Manufacturer, Field.Store.YES, Field.Index.ANALYZED));
            
            return document;
        }

        public void AddDocument(Document document)
        {
            using (_directory = FSDirectory.Open(_directoryInfo))
            {
                using (_indexWriter = new IndexWriter(_directory, _analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    _indexWriter.AddDocument(document);
                }
            }
        }

        public void AddProductToDocument(Product product)
        {
            AddDocument(CreateProductDocument(product));
        }

        public IEnumerable<string> Search(string queryString)
        {
            using (_directory = FSDirectory.Open(_directoryInfo))
            {
                var searcher = new IndexSearcher(_directory, true);
                var queryParser = new MultiFieldQueryParser(Version.LUCENE_30, productSearchFields, _analyzer);

                queryParser.AllowLeadingWildcard = true;
                var parsedQuery = queryParser.Parse("*" + queryString.ToLower() + "*");

                var hits = searcher.Search(parsedQuery, 100).ScoreDocs;

                var ids = hits.Select(hit => searcher.Doc(hit.Doc).Get("Id"));

                return ids;
            }
        }

        public void DeleteDocument(string productId)
        {
            using (_directory = FSDirectory.Open(_directoryInfo))
            {
                using (var indexReader = IndexReader.Open(_directory, false))
                {
                    var searcher = new IndexSearcher(_directory);
                    var queryParser = new QueryParser(Version.LUCENE_30, "Id", _analyzer);
                    var parsedQuery = queryParser.Parse(productId);
                    var hits = searcher.Search(parsedQuery, 1).ScoreDocs;

                    foreach (ScoreDoc hit in hits)
                    {
                        indexReader.DeleteDocument(hit.Doc);
                    }
                }
            }
        }
    }
}