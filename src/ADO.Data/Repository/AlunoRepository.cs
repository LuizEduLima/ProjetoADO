using ADO.Business.Interfaces;
using ADO.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADO.Data.Repository
{
    public class AlunoRepository : Repository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(BaseADO baseADO) : base(baseADO)
        {
        }

    }
}
