using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PI_3.Models
{
    public class Curso {


        public int CursoId { get; set; }
        public int CursoNome { get; set; }
        public string CursoDesc { get; set; }
        
        public ICollection<CursoAluno> CursoAluno { get; set; }

        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }

    }
}