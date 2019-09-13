namespace PI_3.Models
{
    public abstract class Usuario {
        public int UsuarioID { get; set; }
        public string UsuarioNome { get; set; }
        public string UsuarioEmail { get; set; }
        public string UsuarioSenha { get; set; }
    }
}