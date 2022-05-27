using ADO.Business.Interfaces;
using ADO.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADO.Data.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(BaseADO baseADO) : base(baseADO)
        {
        }
    }
}
