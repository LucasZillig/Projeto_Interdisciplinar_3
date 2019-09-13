using System.ComponentModel.DataAnnotations.Schema;

namespace PI_3.Models
{
    public class Aluno {  
        
        public int id_aluno { get; set; }

        public int id_usuario { get; set; }
        [ForeignKey("id_usuario")]
        public Usuario Usuario { get; set; }
    }
}