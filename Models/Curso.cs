namespace PI_3.Models
{
    public class Curso {

        public int id_curso { get; set; }
        public int nome_curso { get; set; }
        public string desc_curso { get; set; }
        
        public int id_professor { get; set; }
    }
}