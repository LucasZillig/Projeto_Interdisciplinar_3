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
                var isProfessor = _context.Professor.Where(e => e.UsuarioId == u.UsuarioId).ToList();

                if(isProfessor.Count > 0){
                    return View("IndexProf");
                }
                return View("Index");
            }
            return View();
        }

    }
}
