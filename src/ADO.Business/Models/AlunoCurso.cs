using System.ComponentModel.DataAnnotations;

namespace ADO.Business.Models
{
    public class AlunoCurso
    {
     
        [Key]
        public int Aluno_id { get; set; }
    
        public int Curso_Id { get; set; }

    }
   
}

