using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace PI_3.Models
{
    public class PerguntaFile{

        public string pergunta { get; set; }
        public IList<IFormFile> files { get; set; }

    }
}