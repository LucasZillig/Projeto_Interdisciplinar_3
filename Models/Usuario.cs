using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PI_3.Models
{
    public class Usuario {

        public int UsuarioId { get; set; }
        public string UsuarioNome { get; set; }
        public string UsuarioEmail { get; set; }
        public string UsuarioSenha { get; set; }
        public string UsuarioToken { get; set; }

        public Aluno Aluno { get; set; }
        public Professor Professor { get; set; }
        
    }
}