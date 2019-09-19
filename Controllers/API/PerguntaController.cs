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
    public class PerguntaController : ControllerBase
    {
        public AppDbContext _context;
        public PerguntaController (AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Pergunta>> GetPerguntas()
        {
            return _context.Pergunta.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Pergunta> GetPergunta(int id)
        {
            var pergunta = _context.Pergunta.SingleOrDefault(x => x.PerguntaId == id);

            if(pergunta == null) {
                return NotFound();
            }

            return pergunta;
        }

        [HttpPost]
        public ActionResult<Pergunta> AddPergunta(Pergunta requestPergunta)
        {
            _context.Pergunta.Add(requestPergunta);
            _context.SaveChanges();

            return requestPergunta;
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePergunta(int id, Pergunta requestPergunta)
        {
            if (id != requestPergunta.PerguntaId)
            {
                return BadRequest();
            }

            var pergunta = _context.Pergunta.SingleOrDefault(x => x.PerguntaId == requestPergunta.PerguntaId);
                
            pergunta.PerguntaNome = requestPergunta.PerguntaNome;
            pergunta.PerguntaDesc = requestPergunta.PerguntaDesc;
            pergunta.Arquivado = requestPergunta.Arquivado;

            _context.Pergunta.Update(pergunta);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePergunta(int id)
        {
            var pergunta = _context.Pergunta.SingleOrDefault(x => x.PerguntaId == id);

            if (pergunta == null)
            {
                return NotFound();
            }

            _context.Pergunta.Remove(pergunta);
            _context.SaveChanges();
            
            return NoContent();
        }
    }
}