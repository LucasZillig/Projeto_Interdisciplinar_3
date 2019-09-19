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
        public ActionResult<IEnumerable<PerguntaArquivo>> GetPerguntasArquivos(int id)
        {
            return _context.PerguntaArquivo.Where(s => s.PerguntaArquivoId == id).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<PerguntaArquivo> GetPerguntaArquivo(int id)
        {
            var PerguntaArquivo = _context.PerguntaArquivo.SingleOrDefault(x => x.PerguntaArquivoId == id);
            
            return PerguntaArquivo;
        }

        [HttpPost]
        public ActionResult<PerguntaArquivo> AddPerguntaArquivo(PerguntaArquivo requestPerguntaArquivo)
        {
            if(requestPerguntaArquivo != null){

                PerguntaArquivo PerguntaArquivo = new PerguntaArquivo();
 
                PerguntaArquivo.PerguntaId = requestPerguntaArquivo.PerguntaId;
                PerguntaArquivo.ArquivoId = requestPerguntaArquivo.ArquivoId;

                _context.PerguntaArquivo.Add(PerguntaArquivo);
                _context.SaveChanges();
                return PerguntaArquivo;
            }
            return null;
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePerguntaArquivo(int id, PerguntaArquivo requestPerguntaArquivo)
        {
            if (id != requestPerguntaArquivo.PerguntaArquivoId)
                return BadRequest();

            var PerguntaArquivo = _context.PerguntaArquivo.SingleOrDefault(x => x.PerguntaArquivoId == requestPerguntaArquivo.PerguntaArquivoId);

            PerguntaArquivo.statusInvite = 0;

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