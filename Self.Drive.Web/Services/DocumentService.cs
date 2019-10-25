using MongoDB.Driver;
using Self.Drive.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Self.Drive.Web.Services
{
    public class DocumentService
    {
        private readonly IMongoCollection<Document> _docs;

        public DocumentService(IDocumentsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _docs = database.GetCollection<Document>(settings.DocumentsCollectionName);
        }

        public List<Document> Get() => _docs.Find(doc => true).ToList();

        public Document Get(string id) => _docs.Find(doc => doc.Id == id).FirstOrDefault();

        public Document Create(Document doc)
        {
            _docs.InsertOne(doc);
            return doc;
        }

        public void Update(string id, Document document)
        {
            _docs.ReplaceOne(doc => doc.Id == id, document);
        }

        public void Remove(Document document)
        {
            _docs.DeleteOne(doc => doc.Id == document.Id);
        }

        public void Remove(string id)
        {
            _docs.DeleteOne(doc => doc.Id == id);
        }
    }
}
