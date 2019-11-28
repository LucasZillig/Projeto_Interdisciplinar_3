using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PI_3.Models;
using PI_3.Request;
using Microsoft.EntityFrameworkCore;

namespace PI_3.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public AppDbContext _context;
        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> GetUsuarios()
        {
            return _context.Usuario.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            var usuario = _context.Usuario.SingleOrDefault(x => x.UsuarioId == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult GetByEmail(string email)
        {
            var Usuarios = _context.Usuario.Include(a => a.Aluno).Where(x => x.UsuarioEmail == email).ToList();
            
            if(Usuarios.Count > 0) 
            {
                var usuario = Usuarios[0];
                usuario.Aluno.Usuario = null;
                return new JsonResult(usuario);
            }else
            {
                return new JsonResult("Esse usuário não existe!") { StatusCode = 404 };
            }
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult CheckCoursePresence(int alunoID, int courseID)
        {
            var alunos = _context.Aluno
                                    .Include(u => u.Usuario)
                                    .Include(c => c.CursoAluno)
                                    .Where(x => x.AlunoId == alunoID)
                                    .ToList();
            
            if(alunos != null) 
            {
                var aluno = alunos[0];
                aluno.Usuario.Aluno = null;
                var cursoAluno = aluno.CursoAluno;

                foreach(CursoAluno c in cursoAluno){
                    if(c.CursoId == courseID){
                        return new JsonResult(new {onCourse = true});
                    }
                }
                return new JsonResult(new {onCourse = false});
            }else
            {
                return new JsonResult(new {onCourse = false});
            }
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult LoginUsuario(string email, string senha)
        {

            if (email != null && senha != null)
            {
                var checkUsuario = _context.Usuario
                                    .Where(e => e.UsuarioEmail == email)
                                    .Where(s => s.UsuarioSenha == senha)
                                    .ToList();
                
                if (checkUsuario.Count > 0)
                {
                    var idStr = checkUsuario[0].UsuarioId.ToString("X8");
                    Guid guid = Guid.NewGuid();
                    string token = guid.ToString("N");
                    //Random rnd = new Random();
                    //Byte[] b = new Byte[24];
                    //rnd.NextBytes(b);
                    //string token = Convert.ToBase64String(b);
                    var cookieStr = idStr + token;

                    CookieOptions option = new CookieOptions();
                    option.MaxAge = TimeSpan.FromMilliseconds(31536000);
                    option.HttpOnly = true;
                    option.Path = "/";
                    option.Secure = false;

                    checkUsuario[0].UsuarioToken = token;
                    _context.Usuario.Update(checkUsuario[0]);
                    _context.SaveChanges();
                    Response.Cookies.Append("Usuario", cookieStr, option);
                    return new JsonResult("Bem vindo, " + checkUsuario[0].UsuarioNome);
                }
                else
                {
                    return new JsonResult("Usuario e/ou senha incorretos") { StatusCode = 403 };
                }
            }
            else
            {
                return new JsonResult("Complete todos os campos") { StatusCode = 400 };
            }
        }

        [HttpGet]
        [Route("[action]")]
        public void LogoutUsuario()
        {
            Response.Cookies.Delete("Usuario");
        }


        [HttpPost]
        [Route("[action]")]
        public ActionResult<Usuario> RegisterAluno([FromBody]UsuarioRequest usuarioRequest)
        {   
            Usuario usuario = new Usuario();
            usuario.UsuarioEmail = usuarioRequest.UsuarioEmail;
            usuario.UsuarioNome = usuarioRequest.UsuarioNome;
            usuario.UsuarioSenha = usuarioRequest.UsuarioSenha;
            
            _context.Usuario.Add(usuario);

            Aluno aluno = new Aluno();

            aluno.UsuarioId = usuario.UsuarioId;
            _context.Aluno.Add(aluno);

            _context.SaveChanges();

            usuario.Aluno.Usuario = null;

            return new JsonResult(usuario);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult RegisterProfessor([FromBody]Usuario requestUsuario)
        {
            _context.Usuario.Add(requestUsuario);

            Professor professor = new Professor();

            professor.UsuarioId = requestUsuario.UsuarioId;
            _context.Professor.Add(professor);

            _context.SaveChanges();

            requestUsuario.Professor.Usuario = null;

            return new JsonResult(requestUsuario);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUsuario(int id, Usuario requestUsuario)
        {
            if (id != requestUsuario.UsuarioId)
            {
                return BadRequest();
            }

            var usuario = _context.Usuario.SingleOrDefault(x => x.UsuarioId == requestUsuario.UsuarioId);

            usuario.UsuarioNome = requestUsuario.UsuarioNome;
            usuario.UsuarioEmail = requestUsuario.UsuarioEmail;
            usuario.UsuarioSenha = requestUsuario.UsuarioSenha;

            // _context.Entry(requestUsuario).State = EntityState.Modified;
            _context.Usuario.Update(usuario);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUsuario(int id)
        {
            var usuario = _context.Usuario.SingleOrDefault(x => x.UsuarioId == id);

            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuario);
            _context.SaveChanges();

            return NoContent();
        }
    }
}