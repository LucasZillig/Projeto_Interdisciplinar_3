using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PI_3.Models;
using PI_3.Services;


namespace PI_3.Controllers
{
    public class HomeController : Controller
    {
        public IValidaCookie _cookie;
        public AppDbContext _context;

        public HomeController(IValidaCookie cookie,AppDbContext context)
        {
            _cookie = new ValidaCookie();
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexProf()
        {
            return View();
        }

        public IActionResult Login()
        {
            Usuario u = _cookie.validarCookie(Request.HttpContext);
            if(u != null){

                ViewBag.Usuario = u;
                
                var professor = _context.Professor.Where(e => e.UsuarioId == u.UsuarioId).ToList();
                var aluno = _context.Aluno.Where(e => e.UsuarioId == u.UsuarioId).ToList();

                if(professor.Count > 0){
                    ViewBag.ProfessorId = professor[0].ProfessorId;
                    return View("IndexProf");
                }
                ViewBag.AlunoId = aluno[0].AlunoId;
                return View("Index");
            }
            return View();
        }

    }
}
