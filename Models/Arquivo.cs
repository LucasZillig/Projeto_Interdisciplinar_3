using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PI_3.Models
{
    public class Arquivo {

        public int ArquivoId { get; set; }
        public int ArquivoTipo { get; set; }
        public string ArquivoUrl { get; set; }

        public ICollection<PerguntaArquivo> ArquivosPergunta { get; set; }

    }
}