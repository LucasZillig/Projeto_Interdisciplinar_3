using System;
using System.Collections.Generic;

namespace PI_3.Models
{
    public class PerguntaArquivo{

        public int PerguntaId { get; set; }
        public Pergunta Pergunta { get; set; }
        
        public int ArquivoId { get; set; }
        public Arquivo Arquivo { get; set; }
    }
}