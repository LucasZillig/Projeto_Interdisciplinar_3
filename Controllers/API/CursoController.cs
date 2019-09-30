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
        public ActionResult<IEnumerable<Curso>> GetCursos()
        {
            return _context.Curso.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Curso> GetCurso(int id)
        {
            var curso = _context.Curso.SingleOrDefault(x => x.CursoId == id);

            if(curso == null){
                return NotFound();
            }

            return curso;
        }

        [HttpPost]
        public ActionResult<Curso> AddCurso(Curso requestCurso)
        {   

            if(requestCurso.CursoNome != "" && requestCurso.CursoDesc != ""){
                _context.Curso.Add(requestCurso);
                _context.SaveChanges();

                return new JsonResult("Curso criado!");
            }else
            {
                return new JsonResult("Complete todos os campos") {
                    StatusCode = 400
                };
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCurso(int id, Curso requestCurso)
        {
            if (id != requestCurso.CursoId)
            {
                return BadRequest();
            }

            var curso = _context.Curso.SingleOrDefault(x => x.CursoId == requestCurso.CursoId);

                curso.CursoNome = requestCurso.CursoNome;
                curso.CursoDesc = requestCurso.CursoDesc;

                _context.Curso.Update(curso);
                _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCurso(int id)
        {
            var curso = _context.Curso.SingleOrDefault(x => x.CursoId == id);

            if (curso == null)
            {
                return NotFound();
            }

            _context.Curso.Remove(curso);
            _context.SaveChanges();
            
            return NoContent();
        }
    }
}