namespace PI_3.Models
{
    public abstract class Usuario {

        public int id_usuario { get; set; }
        public string nome_usuario { get; set; }
        public string email_usuario { get; set; }
        public string senha_usuario { get; set; }
    }
}