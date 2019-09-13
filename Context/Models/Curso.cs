using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_with_EF.Context.Models
{
    public class Curso
    {
        public int CursoID { get; set; }
        public int CursoNome { get; set; }
        public string CursoDesc { get; set; }
        public string CursoTag { get; set; }

        public int ProfessorID { get; set; }
        public Professor Professor { get; set; }

        public List<Turma> Turmas { get; set; }
    }
}
