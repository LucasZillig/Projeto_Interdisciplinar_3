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
    public class CursoController : ControllerBase
    {
        public AppDbContext _context;
        public CursoController (AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Curso>> Get()
        {
            return _context.Curso.ToList();
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult GetCursosProfessor(int professorId)
        {
            var cursos = _context.Curso.Where(x => x.ProfessorId == professorId).ToList();
            return new JsonResult(cursos);
        }

        [HttpGet("{id}")]
        public ActionResult<Curso> Get(int id)
        {
            var curso = _context.Curso.SingleOrDefault(x => x.CursoId == id);

            if(curso == null){
                return NotFound();
            }

            return curso;
        }


        [HttpPost]
        public ActionResult<Curso> Add(Curso requestCurso)
        {   

            if( string.IsNullOrWhiteSpace(requestCurso.CursoNome) )
            {
                return new JsonResult("Complete todos os campos") { StatusCode = 400 };
            }

            _context.Curso.Add(requestCurso);
            _context.SaveChanges();

            return new JsonResult("Curso criado!");
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Curso requestCurso)
        {
            if (id != requestCurso.CursoId)
            {
                return BadRequest();
            }

            var curso = _context.Curso.SingleOrDefault(x => x.CursoId == requestCurso.CursoId);

            curso.CursoNome = requestCurso.CursoNome;
            curso.CursoTag = requestCurso.CursoTag;

            _context.Curso.Update(curso);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var curso = _context.Curso.SingleOrDefault(x => x.CursoId == id);

            if (curso == null)
            {
                return new JsonResult("Curso não encontrado") { StatusCode = 404 };
            }

            _context.Curso.Remove(curso);
            _context.SaveChanges();
            
            return new JsonResult("Curso deletado!");
        }
    }
}