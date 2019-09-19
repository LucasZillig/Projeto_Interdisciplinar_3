using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_with_EF.Context.Models
{
    public class Professor
    {
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public int ProfessorID { get; set; }

        public List<Usuario> Usuarios { get; set; }

    }
}
