using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_with_EF.Context.Models
{
    public class Aluno
    {
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public int AlunoID { get; set; }

        public List<Usuario> Usuarios { get; set; }

    }
}
