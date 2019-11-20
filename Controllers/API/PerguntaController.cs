using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PI_3.Models;
using Microsoft.EntityFrameworkCore;

namespace PI_3.Controllers.API
{   
    [Route("api/[controller]")]
    [ApiController] 
    public class PerguntaController : ControllerBase
    {
        public AppDbContext _context;
        private IHostingEnvironment _hostingEnvironment;
        public PerguntaController (AppDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
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

        [HttpGet]
        [Route("[action]")]
        public ActionResult<IEnumerable<Pergunta>> GetPerguntaByAluno(int user_id)
        {
            var perguntas = _context.Pergunta
                                .Include(c => c.CursoAluno)
                                    .ThenInclude(c => c.Aluno)
                                    .ThenInclude(u => u.Usuario)
                                .Include(c2 => c2.CursoAluno)
                                    .ThenInclude(c2 => c2.Curso)
                                    .ThenInclude(p2 => p2.Professor)
                                .Where(x => x.CursoAluno.AlunoId == user_id)
                                .ToList();

            if(perguntas == null) {
                return NotFound();
            }

            foreach (Pergunta pergunta in perguntas)
            {
                 pergunta.CursoAluno.Perguntas = null;
                 pergunta.CursoAluno.Aluno.CursoAluno = null;
                 pergunta.CursoAluno.Aluno.Usuario.Aluno = null;
                 pergunta.CursoAluno.Curso.CursoAluno = null;
                 pergunta.CursoAluno.Curso.Professor.Cursos = null;
            }

            return perguntas;
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult<IEnumerable<Pergunta>> GetPerguntaByProfessor(int user_id)
        {
            var perguntas = _context.Pergunta
                                .Include(c => c.CursoAluno)
                                    .ThenInclude(c => c.Aluno)
                                    .ThenInclude(u => u.Usuario)
                                .Include(c2 => c2.CursoAluno)
                                    .ThenInclude(c2 => c2.Curso)
                                    .ThenInclude(p2 => p2.Professor)
                                .Where(x => x.CursoAluno.Curso.Professor.ProfessorId == user_id)
                                .ToList();

            if(perguntas == null) {
                return NotFound();
            }

            foreach (Pergunta pergunta in perguntas)
            {
                 pergunta.CursoAluno.Perguntas = null;
                 pergunta.CursoAluno.Aluno.CursoAluno = null;
                 pergunta.CursoAluno.Aluno.Usuario.Aluno = null;
                 pergunta.CursoAluno.Curso.CursoAluno = null;
                 pergunta.CursoAluno.Curso.Professor.Cursos = null;
            }

            return perguntas;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult AddPergunta([FromBody]Pergunta pergunta)
        {   
            pergunta.PerguntaData = DateTime.Now;
            _context.Pergunta.Add(pergunta);
            _context.SaveChanges();

            return new JsonResult(pergunta);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult UploadArquivo(IFormFile file)
        {
            string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            filename = this.EnsureFilename(filename);
            using (FileStream filestream = System.IO.File.Create(this.GetPath(filename, 1)))
            {

            }

            return new JsonResult("Arquivo enviado");
        }

        private string GetPath(string filename, int id)
        {
            string path = $"{_hostingEnvironment.WebRootPath}\\Perguntas\\{id}\\";
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