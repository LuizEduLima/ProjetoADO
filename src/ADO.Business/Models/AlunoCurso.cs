using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ADO.Business.Models
{
    public class AlunoCurso
    {

        [Required(ErrorMessage = "O Aluno tem que ser informado...")]
        public int Aluno_id { get; set; }

        [Required(ErrorMessage = "O Curso tem que ser informado...")]
        public int Curso_Id { get; set; }


        [Write(false)]
        public ICollection<Aluno> alunos { get; set; }
        [Write(false)]
        public ICollection<Curso> cursos { get; set; }

        public AlunoCurso()
        {
            alunos ??= new List<Aluno>();
            cursos ??= new List<Curso>();
        }
    }

}

