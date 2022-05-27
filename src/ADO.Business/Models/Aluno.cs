using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ADO.Business.Models
{
    public  class Aluno:Entity
    {
       
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Nascimento { get; set; }

    }
}
