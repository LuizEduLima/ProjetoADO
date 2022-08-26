using ADO.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Business.Interfaces
{
    public interface IAlunoRepository:IRepository<Aluno>
    {
        Task<bool> ObterBool(int id);
    }
}
