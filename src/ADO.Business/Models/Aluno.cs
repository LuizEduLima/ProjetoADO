using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ADO.Business.Models
{
    public  class Aluno:Entity
    {
       
        [Required(ErrorMessage ="O {0} é obrigatõrio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatõrio")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatõrio")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatõrio")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatõrio")]
        public string Nascimento { get; set; }

    }
}
