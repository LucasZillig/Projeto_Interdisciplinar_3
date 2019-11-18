using System.Collections.Generic;

namespace PI_3.Models
{
    public class Aluno {  
        public int AlunoId { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<CursoAluno> CursoAluno { get; set; }
    }
}