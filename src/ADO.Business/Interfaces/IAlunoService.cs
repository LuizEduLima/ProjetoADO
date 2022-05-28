using ADO.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Business.Interfaces
{
    public interface IAlunoService
    {
        Task<Aluno> Adicionar(Aluno aluno);
        Task<Aluno> Atualizar(Aluno aluno);
        Task Excluir(int id);
    }
}
