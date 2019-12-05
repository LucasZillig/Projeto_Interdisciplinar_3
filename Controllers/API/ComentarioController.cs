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
    public class ComentarioController : ControllerBase
    {
        public AppDbContext _context;
        public ComentarioController (AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Comentario>> GetComentarios()
        {
            return _context.Comentario.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Comentario> GetComentario(int id)
        { 
            var comentario = _context.Comentario.SingleOrDefault(x => x.ComentarioId == id);

            if(comentario == null){
                return NotFound();
            }

            return comentario;
        }

        [HttpPost]
        public ActionResult<Comentario> AddComentario([FromBody]Comentario comentario)
        {
            comentario.ComentarioData = DateTime.Now;
            _context.Comentario.Add(comentario);
            _context.SaveChanges();

            return new JsonResult(comentario);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateComentario(int id, Comentario requestComentario)
        {
            if (id != requestComentario.ComentarioId)
            {
                return BadRequest();
            }

            var comentario = _context.Comentario.SingleOrDefault(x => x.ComentarioId == requestComentario.ComentarioId);
                //Update campos
            _context.Comentario.Update(comentario);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteComentario(int id)
        {
            var comentario = _context.Comentario.SingleOrDefault(x => x.ComentarioId == id);

            if (comentario == null)
            {
                return NotFound();
            }

            _context.Comentario.Remove(comentario);
            _context.SaveChanges();
            
            return NoContent();
        }
    }
}