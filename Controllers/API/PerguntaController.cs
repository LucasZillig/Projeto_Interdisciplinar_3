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
using PI_3.Response;

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
        public IActionResult UploadArquivo()
        {
            Console.WriteLine(Request.Form.Files.Count);
            Console.WriteLine(Request.Form["id"]);


            if (!int.TryParse(Request.Form["id"], out int id)) {
                // erro!
            }

            string path = System.IO.Path.Join(_hostingEnvironment.WebRootPath, "Arquivos", id.ToString());
            if (!System.IO.Directory.Exists(path)) {
                System.IO.Directory.CreateDirectory(path);
            }

            using (System.IO.Stream stream = Request.Form.Files[0].OpenReadStream()) {
                using (System.IO.FileStream destination = new System.IO.FileStream(System.IO.Path.Join(path, Request.Form.Files[0].FileName), FileMode.Create)) {
                    stream.CopyTo(destination);
                }
            }
            
            return new JsonResult("Arquivo enviado");
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetFiles(int id)
        {
            List<FilesResponse> returnFiles = new List<FilesResponse>();

            string path = System.IO.Path.Join(_hostingEnvironment.WebRootPath, "Arquivos", (id).ToString());
            if (!System.IO.Directory.Exists(path)) {
                return new JsonResult("Essa pergunta não possui arquivos!") { StatusCode = 404};
            }

            string[] filePaths = Directory.GetFiles(@path);
            foreach(string filePath in filePaths){
                returnFiles.Add(new FilesResponse{
                    Nome = Path.GetFileName(filePath) ,
                    Conteudo = System.IO.File.ReadAllText(filePath)
                });

            }

            return new JsonResult(returnFiles);
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePergunta(int id, Pergunta requestPergunta)
        {
            if (id != requestPergunta.PerguntaId)
            {
                return BadRequest();
            }

            var perguntas = _context.Pergunta.Where(x => x.PerguntaId == requestPergunta.PerguntaId).ToList();
            var pergunta = perguntas[0];

            if(requestPergunta.Arquivado == 1){
                pergunta.Arquivado = 0;
            }else{
                pergunta.Arquivado = 1;
            }
            
            _context.Pergunta.Update(pergunta);
            _context.SaveChanges();

            return new JsonResult("Pergunta" + (pergunta.Arquivado == 1 ? " arquivada" : " não arquivada"));
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