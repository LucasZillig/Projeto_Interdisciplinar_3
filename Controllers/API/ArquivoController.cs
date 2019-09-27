// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using Microsoft.AspNetCore.Mvc;
// using PI_3.Models;

// namespace PI_3.Controllers.API {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class ArquivoController : ControllerBase
//     {
//         public AppDbContext _context;

//         public ArquivoController (AppDbContext context)
//         {
//             _context = context;
//         }
        
//         [HttpGet]
//         public ActionResult<IEnumerable<Arquivo>> GetComentarios()
//         {
//             return _context.Arquivo.ToList();
//         }

//         [HttpGet("{id}")]
//         public ActionResult<Arquivo> GetArquivo(int id)
//         {            
//             return _context.Arquivo.SingleOrDefault(i => i.ArquivoId == id);
//         }

//         [HttpPost]
//         public ActionResult<Arquivo> AddArquivo(Arquivo requestArquivo)
//         {
//             _context.Arquivo.Add(requestArquivo);

//             _context.SaveChanges();

//             return new JsonResult(requestArquivo);
//         }

//         [HttpPut("{id}")]
//         public ActionResult UpdateArquivo(int id, Arquivo requestArquivo)
//         {
//             if (id != requestArquivo.ArquivoId)
//                 return BadRequest();

//             var arquivo = _context.Arquivo.SingleOrDefault(x => x.ArquivoId == requestArquivo.ArquivoId);
//             arquivo.ArquivoTipo = requestArquivo.ArquivoTipo;
//             arquivo.ArquivoUrl = requestArquivo.ArquivoUrl;

//             _context.Arquivo.Update(arquivo);
//             _context.SaveChanges();

//             return NoContent();
//         }

//         [HttpDelete("{id}")]
//         public ActionResult DeleteArquivo(int id) {
//             var arquivo = _context.Arquivo.SingleOrDefault(x => x.ArquivoId == id);

//             if (arquivo == null)
//             {
//                 return NotFound();
//             }

//             _context.Arquivo.Remove(arquivo);
//             _context.SaveChanges();

//             return NoContent();
//         }
//     }
// }