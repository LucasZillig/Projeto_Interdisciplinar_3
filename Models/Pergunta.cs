using System;

namespace PI_3.Models
{
    public class Pergunta{

        public int id_pergunta { get; set; }
        public string nome_pergunta { get; set; }
        public string desc_pergunta { get; set; }
        public bool arquivado { get; set; }
        public DateTime data_pergunta { get; set; }

        public int id_turma { get; set; }
    }
}