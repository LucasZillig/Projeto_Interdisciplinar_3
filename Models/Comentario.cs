using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PI_3.Models
{
    public class Comentario {

        public int ComentarioId { get; set; }
        public DateTime ComentarioData { get; set; }
        public string ComentarioConteudo { get; set; }
        public int PerguntaId { get; set; }
        public Pergunta Pergunta { get; set; }

        public int UsuarioId { get; set; }
    }
}