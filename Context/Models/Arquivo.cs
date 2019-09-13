using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_with_EF.Context.Models
{
    public class Arquivo
    {
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public int ArquivoID { get; set; }
        public int ArquivoTipo { get; set; }
        public string ArquivoURL { get; set; }

        public List<Anexo> Anexos { get; set; }
    }
}
