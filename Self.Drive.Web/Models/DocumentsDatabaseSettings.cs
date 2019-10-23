using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Self.Drive.Web.Models
{
    public class DocumentsDatabaseSettings : IDocumentsDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string DocumentsCollectionName { get; set; }
    }

    public interface IDocumentsDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string DocumentsCollectionName { get; set; }
    }
}
