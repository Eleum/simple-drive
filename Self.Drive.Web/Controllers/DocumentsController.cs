using Microsoft.AspNetCore.Mvc;
using Self.Drive.Web.Models;
using Self.Drive.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Self.Drive.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentService _documentService;

        public DocumentsController(DocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public ActionResult<List<Document>> Get()
        {
            return _documentService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetDocument")]
        public ActionResult<Document> Get(string id)
        {
            var doc = _documentService.Get(id);

            if (doc == null)
                return NotFound();
            
            return doc;
        }

        [HttpPost]
        public ActionResult<Document> Create(Document doc)
        {
            _documentService.Create(doc);

            return CreatedAtRoute("GetDocument", new { id = doc.Id.ToString() }, doc);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Document docIn)
        {
            var doc = _documentService.Get(id);

            if (doc == null)
                return NotFound();

            _documentService.Update(id, doc);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var doc = _documentService.Get(id);

            if (doc == null)
                return NotFound();

            _documentService.Remove(doc.Id);

            return NoContent(); 
        }
    }
}
