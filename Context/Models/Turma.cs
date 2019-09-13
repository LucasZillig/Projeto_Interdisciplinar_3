using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_with_EF.Context.Models
{
    public class Turma
    {
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public int TurmaID { get; set; }
        public bool TurmaStatusInvite { get; set; }

        public int CursoID { get; set; }
        public Curso Curso { get; set; }

        public int AlunoID { get; set; }
        public Aluno Aluno { get; set; }

        public List<Pergunta> Perguntas { get; set; }

    }
}
