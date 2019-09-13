using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_with_EF.Context.Models
{
    public class Comentario
    {
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public int ComentarioID { get; set; }
        public int ComentarioData { get; set; }
        public string ComentarioConteudo { get; set; }

        public int PerguntaID { get; set; }
        public Pergunta Pergunta { get; set; }

    }
}
