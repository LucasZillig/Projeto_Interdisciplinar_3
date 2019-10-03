using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using MySql.Data.EntityFrameworkCore;
using PI_3.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using PI_3.Services;

namespace PI_3
{
    public static class ValidaCookie
    {
        public static Usuario validarCookie(HttpContext req, AppDbContext context)
        {
            var cookieStr = req.Request.Cookies["Usuario"];

            if (cookieStr == null || cookieStr.Length != 40)
            {
                return null;
            }
            else
            {
                var id = Int32.Parse(cookieStr.Substring(0, 8));
                var user = context.Usuario.Where(u => u.UsuarioId == id).ToList();
                if (user.Count == 0)
                {
                    return null;
                }
                var token = cookieStr.Substring(8);

                if (user[0].UsuarioToken == null ||  token != user[0].UsuarioToken)
                {
                    return null;
                }


                return user[0];
            }
        }

        
    }
}