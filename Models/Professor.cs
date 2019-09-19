using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PI_3.Models
{
    public class Professor {
        
        public int ProfessorId { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<Curso> Cursos { get; set; }
    }
}
