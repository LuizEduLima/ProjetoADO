using ADO.Business.Interfaces;
using ADO.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADO.Data.Repository
{
    public class CursoRepository : Repository<Curso>, ICursoRepository
    {
        public CursoRepository(BaseADO baseADO) : base(baseADO)
        {
        }
    }
}
