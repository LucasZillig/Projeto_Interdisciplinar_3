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
    public class CursoAlunoController : ControllerBase
    {
        public AppDbContext _context;
        public CursoAlunoController (AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<CursoAluno>> GetCursosAlunos(int id)
        {
            return _context.CursoAluno.Where(s => s.CursoAlunoId == id).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<CursoAluno> GetCursoAluno(int id)
        {
            var cursoAluno = _context.CursoAluno.SingleOrDefault(x => x.CursoAlunoId == id);
            
            return cursoAluno;
        }

        [HttpPost]
        public ActionResult<CursoAluno> AddCursoAluno(CursoAluno requestCursoAluno)
        {
            if(requestCursoAluno != null)
            {
                _context.CursoAluno.Add(requestCursoAluno);
                _context.SaveChanges();

                return requestCursoAluno;
            }
            return null;
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCursoAluno(int id, CursoAluno requestCursoAluno)
        {
            if (id != requestCursoAluno.CursoAlunoId)
            {
                return BadRequest();
            }

            var cursoAluno = _context.CursoAluno.SingleOrDefault(x => x.CursoAlunoId == requestCursoAluno.CursoAlunoId);

            cursoAluno.statusInvite = 0;

            _context.CursoAluno.Update(cursoAluno);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCursoAluno(int id)
        {
            var cursoAluno = _context.CursoAluno.SingleOrDefault(x => x.CursoAlunoId == id);

            if (cursoAluno == null)
            {
                return NotFound();
            }

            _context.CursoAluno.Remove(cursoAluno);
            _context.SaveChanges();
            
            return NoContent();
        }
    }
}