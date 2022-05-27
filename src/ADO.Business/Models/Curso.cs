using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ADO.Business.Models
{
    public class Curso : Entity
    {
        public Categoria Categoria { get; set; }
        public string Descricao { get; set; }
        public int TotalHoras { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }

        public int CategoriaId { get; set; }

        [NotMapped]
        [ScaffoldColumn(false)]
        public IEnumerable<Categoria> Categorias { get; set; }= new List<Categoria>();
    }
 
}

