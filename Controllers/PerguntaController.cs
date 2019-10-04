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
    public class PerguntaController : BaseController
    {
        public PerguntaController(AppDbContext context) : base(context)
        {
        }

        public IActionResult Index()
        {
            return View("Question");
        }

    }
}
