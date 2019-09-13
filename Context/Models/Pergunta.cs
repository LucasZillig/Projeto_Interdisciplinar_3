using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_with_EF.Context.Models
{
    public class Pergunta
    {
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public int PerguntaID { get; set; }
        public string PerguntaNome { get; set; }
        public string PerguntaDesc { get; set; }
        public bool PerguntaArquivado { get; set; }
        public DateTime PerguntaData { get; set; }

        public int TurmaID { get; set; }
        public Turma Turma { get; set; }

        public List<Comentario> Comentarios { get; set; }
        public List<Anexo> Anexos { get; set; }

    }
}
