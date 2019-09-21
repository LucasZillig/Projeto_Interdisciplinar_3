using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PI_3.Models;

namespace PI_3.Controllers.API
{   
    [Route("api/[controller]")]
    [ApiController] 
    public class PerguntaArquivo : ControllerBase
    {
        public AppDbContext _context;

        private IHostingEnvironment _hostingEnvironment;

        public PerguntaArquivo (AppDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Models.PerguntaArquivo>> GetPerguntasArquivos()
        {
            return _context.PerguntaArquivo.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Models.PerguntaArquivo> GetPerguntaArquivo(int id)
        {

            var perguntaArquivo = _context.PerguntaArquivo.SingleOrDefault(i => i.PerguntaArquivoId == id);
            
            return perguntaArquivo;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<Models.PerguntaArquivo> AddPerguntaArquivo(Models.PerguntaArquivo requestPerguntaArquivo)
        {
            _context.PerguntaArquivo.Add(requestPerguntaArquivo);

            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPerguntaArquivo), new { id = requestPerguntaArquivo.PerguntaArquivoId }, requestPerguntaArquivo);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult UploadArquivo(IList<IFormFile> files)
        {
            foreach (IFormFile item in files)
            {
                string filename = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                filename = this.EnsureFilename(filename);
                using (FileStream filestream = System.IO.File.Create(this.GetPath(filename)))
                {

                }
            }
            return this.Content("Sucesso");
        }

        private string GetPath(string filename)
        {
            string path = _hostingEnvironment.WebRootPath + "\\upload\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path + filename;
        }

        private string EnsureFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            return filename;
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePerguntaArquivo(int id, Models.PerguntaArquivo requestPerguntaArquivo)
        {
            if (id != requestPerguntaArquivo.PerguntaArquivoId)
                return BadRequest();

            var PerguntaArquivo = _context.PerguntaArquivo.SingleOrDefault(x => x.PerguntaArquivoId == requestPerguntaArquivo.PerguntaArquivoId);

            _context.PerguntaArquivo.Update(PerguntaArquivo);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePerguntaArquivo(int id)
        {
            var PerguntaArquivo = _context.PerguntaArquivo.SingleOrDefault(x => x.PerguntaArquivoId == id);

            if (PerguntaArquivo == null)
                return NotFound();

            _context.PerguntaArquivo.Remove(PerguntaArquivo);
            _context.SaveChanges();
            
            return NoContent();
        }
    }
}