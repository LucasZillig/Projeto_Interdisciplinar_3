using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using PI_3.Models;

namespace PI_3.Services
{
    public interface IValidaCookie
    {
        Usuario validarCookie(HttpContext httpContext);
    }
}