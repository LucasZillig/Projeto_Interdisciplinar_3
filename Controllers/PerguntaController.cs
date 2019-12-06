using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PI_3.Models;
using Microsoft.EntityFrameworkCore;


namespace PI_3.Controllers
{
    public class PerguntaController : BaseController
    {
        public PerguntaController(AppDbContext context) : base(context)
        {
        }
        public IActionResult Index(int id)
        {
            if(Usuario == null)
            {
                return View("Login");
            }
            
            var perguntas = _context.Pergunta
                                .Include(c3 => c3.Comentarios)
                                .Include(c => c.CursoAluno)
                                    .ThenInclude(c => c.Aluno)
                                    .ThenInclude(u => u.Usuario)
                                .Include(c2 => c2.CursoAluno)
                                    .ThenInclude(c2 => c2.Curso)
                                    .ThenInclude(p2 => p2.Professor)
                                .Where(x => x.PerguntaId == id)
                                .ToList();

            
            var pergunta = perguntas[0];
            var comentarios = _context.Comentario.Where(i => i.PerguntaId == pergunta.PerguntaId).ToList();

            pergunta.CursoAluno.Perguntas = null;
            pergunta.CursoAluno.Aluno.CursoAluno = null;
            pergunta.CursoAluno.Aluno.Usuario.Aluno = null;
            pergunta.CursoAluno.Curso.CursoAluno = null;
            pergunta.CursoAluno.Curso.Professor.Cursos = null;

            foreach (Comentario comentario in pergunta.Comentarios)
            {
                comentario.Pergunta.Comentarios = null;
            }

            ViewBag.Comentarios = comentarios;    
            ViewBag.Usuario = Usuario;
            ViewBag.Pergunta = pergunta;        
            return View("Index");
        }

    }
}
