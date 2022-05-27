using System.Collections.Generic;

namespace ADO.Business.Models
{
    public class Categoria : Entity
    {
        public string Descricao { get; set; }

        public ICollection<Curso> cursos { get; set; } = new List<Curso>();

    }

   
}

