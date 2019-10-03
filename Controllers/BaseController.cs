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
    public class BaseController : Controller
    {
        protected Usuario Usuario { private set; get; }
        protected AppDbContext _context;

        public BaseController(AppDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context) {
            Usuario = ValidaCookie.validarCookie(context.HttpContext, _context);
            
            base.OnActionExecuting(context);
        }

    }
}
