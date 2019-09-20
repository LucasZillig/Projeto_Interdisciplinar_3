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


namespace PI_3.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public AppDbContext _context;
        public UsuarioController(AppDbContext context, IValidaCookie cookie)
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

            if (email != null || senha != null)
            {
                var checkUsuario = _context.Usuario
                                    .Where(e => e.UsuarioEmail == email)
                                    .Where(s => s.UsuarioSenha == senha)
                                    .ToList();
                if (checkUsuario[0].UsuarioId > 0)
                {
                    var idStr = "0000000" + String.Concat((checkUsuario[0].UsuarioId).ToString("X"));
                    Random rnd = new Random();
                    Byte[] b = new Byte[22];
                    rnd.NextBytes(b);
                    string token = Convert.ToBase64String(b);
                    var cookieStr = idStr.Substring(idStr.Length - 8) + token;
                    CookieOptions option = new CookieOptions();
                    option.MaxAge = TimeSpan.FromMilliseconds(31536000);
                    option.HttpOnly = true;
                    option.Path = "/";
                    option.Secure = false;

                    var usuarioNovoToken = _context.Usuario.SingleOrDefault(x => x.UsuarioId == checkUsuario[0].UsuarioId);
                    usuarioNovoToken.UsuarioToken = cookieStr;
                    _context.Usuario.Update(usuarioNovoToken);
                    _context.SaveChanges();
                    Response.Cookies.Append("usuario", cookieStr, option);
                    return Ok("Bem vindo, " + checkUsuario[0].UsuarioNome);
                }
                else
                {
                    return Forbid("Usuario e/ou senha incorretos");
                }
            }
            else
            {
                return BadRequest("Complete todos os campos");
            }
        }

        [HttpGet]
        [Route("[action]")]
        public void LogoutUsuario()
        {
            Response.Cookies.Delete("usuario");
        }


        [HttpPost]
        [Route("[action]")]
        public ActionResult<Usuario> RegisterAluno([FromBody]Usuario requestUsuario)
        {
            _context.Usuario.Add(requestUsuario);

            Aluno aluno = new Aluno();

            aluno.UsuarioId = requestUsuario.UsuarioId;
            _context.Aluno.Add(aluno);

            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUsuario), new { id = requestUsuario.UsuarioId }, requestUsuario);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<Usuario> RegisterProfessor([FromBody]Usuario requestUsuario)
        {
            _context.Usuario.Add(requestUsuario);

            Professor professor = new Professor();

            professor.UsuarioId = requestUsuario.UsuarioId;
            _context.Professor.Add(professor);

            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUsuario), new { id = requestUsuario.UsuarioId }, requestUsuario);
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