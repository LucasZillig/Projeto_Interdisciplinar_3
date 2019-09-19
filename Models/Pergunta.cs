using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PI_3.Models
{
    public class Pergunta{



        public int PerguntaId { get; set; }
        public string PerguntaNome { get; set; }
        public string PerguntaDesc { get; set; }
        public int Arquivado { get; set; }
        public DateTime PerguntaData { get; set; }

        public int CursoAlunoId { get; set; }
        public CursoAluno CursoAluno { get; set; }

        public ICollection<Comentario> Comentarios { get; set; }
        public ICollection<PerguntaArquivo> ArquivosPergunta { get; set; }


    }
}