using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PI_3.Models
{
    public class CursoAluno {

        public int CursoAlunoId { get; set; }
        public int statusInvite { get; set; }
        public string CursoAlunoTag { get; set; }

        public int CursoId { get; set; }
        public Curso Curso { get; set; }
    
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }
        public ICollection<Pergunta> Perguntas { get; set; }

    }
}