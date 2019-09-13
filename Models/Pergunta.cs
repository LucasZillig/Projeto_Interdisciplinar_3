using System;

namespace PI_3.Models
{
    public class Pergunta{
        public int PerguntaID { get; set; }
        public string PerguntaNome { get; set; }
        public string PerguntaDesc { get; set; }
        public bool PerguntaArquivado { get; set; }
        public DateTime PerguntaData { get; set; }
    }
}