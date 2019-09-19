using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PI_3.Models;

namespace PI_3.Controllers.API
{   
    [Route("api/[controller]")]
    [ApiController] 
    public class PerguntaArquivoController : ControllerBase
    {
        public AppDbContext _context;
        public PerguntaArquivoController (AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<PerguntaArquivo>> GetPerguntasArquivos()
        {
            return _context.PerguntaArquivo.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<PerguntaArquivo> GetPerguntaArquivo(int id)
        {

            var perguntaArquivo = _context.PerguntaArquivo.SingleOrDefault(i => i.PerguntaArquivoId == id);
            
            return perguntaArquivo;
        }

        [HttpPost]
        public ActionResult<PerguntaArquivo> AddPerguntaArquivo(PerguntaArquivo requestPerguntaArquivo)
        {
            _context.PerguntaArquivo.Add(requestPerguntaArquivo);

            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPerguntaArquivo), new { id = requestPerguntaArquivo.PerguntaArquivoId }, requestPerguntaArquivo);
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePerguntaArquivo(int id, PerguntaArquivo requestPerguntaArquivo)
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