using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_with_EF.Context.Models
{
    public class Usuario
    {
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public int UsuarioID { get; set; }
        public string UsuarioNome { get; set; }
        public string UsuarioEmail { get; set; }
        public string UsuarioSenha { get; set; }

        public int ProfessorID { get; set; }
        public Professor Professor { get; set; }

        public int AlunoID { get; set; }
        public Aluno Aluno { get; set; }

    }
}
