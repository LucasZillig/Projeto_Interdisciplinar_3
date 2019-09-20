using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PI_3.Models;



namespace PI_3.Controllers
{
    public class HomeController : Controller
    {
        public IValidaCookie _cookie;

        public HomeController(IValidaCookie cookie)
        {
            _cookie = new ValidaCookie();

        }

        public IActionResult Index()
        {
            
            Usuario u = _cookie.validarCookie(Request.HttpContext);
            return View();
        }

        public IActionResult Login()
        {
            Usuario u = _cookie.validarCookie(Request.HttpContext);
            if(u != null){
                ViewBag.Usuario = u;
                return View("Index");
            }
            
            return View();
        }

    }
}
