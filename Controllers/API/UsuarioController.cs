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
            return _context.Usuario.ToList();;
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
        public ActionResult<Usuario> AddAluno(Usuario requestUsuario)
        {
            if (requestUsuario == null)
            {
                return null;
            }

            Usuario usuario = new Usuario();

            usuario.UsuarioNome = requestUsuario.UsuarioNome;
            usuario.UsuarioSobrenome = requestUsuario.UsuarioSobrenome;
            usuario.UsuarioEmail = requestUsuario.UsuarioEmail;
            usuario.UsuarioSenha = requestUsuario.UsuarioSenha;

            _context.Usuario.Add(usuario);

            Aluno aluno = new Aluno();
            
            aluno.UsuarioId = usuario.UsuarioId;
            _context.Aluno.Add(aluno);

            _context.SaveChanges();

            return usuario; 
        }

        [HttpPost]
        public ActionResult<Usuario> AddProfessor(Usuario requestUsuario)
        {
            if (requestUsuario == null)
            {
                return null;
            }

            Usuario usuario = new Usuario();

            usuario.UsuarioNome = requestUsuario.UsuarioNome;
            usuario.UsuarioSobrenome = requestUsuario.UsuarioSobrenome;
            usuario.UsuarioEmail = requestUsuario.UsuarioEmail;
            usuario.UsuarioSenha = requestUsuario.UsuarioSenha;

            _context.Usuario.Add(usuario);

            Professor professor = new Professor();

            professor.UsuarioId = usuario.UsuarioId;
            _context.Professor.Add(professor);

            _context.SaveChanges();

            return usuario;
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
            usuario.UsuarioSobrenome = requestUsuario.UsuarioSobrenome;
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