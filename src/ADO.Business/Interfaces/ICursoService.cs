using ADO.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Business.Interfaces
{
    public interface ICursoService
    {
        Task<Curso> Adicionar(Curso curso);
        Task<Curso> Atualizar(Curso curso);
        Task Excluir(int id);
    }
}
