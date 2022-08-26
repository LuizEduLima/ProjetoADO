using ADO.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Business.Interfaces
{
    public interface ICursoRepository:IRepository<Curso>
    {
        Task<IEnumerable<Curso>> ObterAlunosPorCursos();
        Task MatricularAlunoCurso(int alunoId, int cursoId);

    }
}
