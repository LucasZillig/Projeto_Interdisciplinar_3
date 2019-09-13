using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_with_EF.Context.Models
{
    public class Anexo
    {
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public int PerguntaID { get; set; }
        public Pergunta Pergunta { get; set; }

        public int ArquivoID { get; set; }
        public Arquivo Arquivo { get; set; }
    }
}
