using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PI_3.Models;
using System.Web;
using System.Net.Mime;

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
                    return new JsonResult("Usuario e/ou senha incorretos") {
                        StatusCode = 403
                    };
                }
            }
            else
            {
                return new JsonResult("Complete todos os campos") {
                    StatusCode = 400
                };
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
        public ActionResult RegisterAluno([FromBody]Usuario requestUsuario)
        {   
            _context.Usuario.Add(requestUsuario);

            Aluno aluno = new Aluno();

            aluno.UsuarioId = requestUsuario.UsuarioId;
            _context.Aluno.Add(aluno);

            _context.SaveChanges();

            requestUsuario.Aluno.Usuario = null;

            return new JsonResult(requestUsuario);
        }


        //POR QUE STRINGFY? COMO FAZER CORRETAMENTE? TIRAR DATATYPE?
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

            usuario.UsuarioSenha = requestUsuario.UsuarioSenha;
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