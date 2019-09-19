using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PI_3.Models;



namespace PI_3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
             return View();
        }

        public IActionResult Login()
        {
             return View();
        }

    }
}
