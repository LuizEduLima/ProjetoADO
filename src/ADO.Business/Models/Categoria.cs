using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADO.Business.Models
{
    public class Categoria : Entity
    {
        public string Descricao { get; set; }

        [NotMapped]
        public ICollection<Curso> cursos { get; set; } = new List<Curso>();

    }

   
}

