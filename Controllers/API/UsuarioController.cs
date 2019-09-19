using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PI_3.Models;

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

        [HttpPost]
        [Route("[action]")]
        public ActionResult<Usuario> AddAluno([FromBody]Usuario requestUsuario)
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
        public ActionResult<Usuario> AddProfessor([FromBody]Usuario requestUsuario)
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