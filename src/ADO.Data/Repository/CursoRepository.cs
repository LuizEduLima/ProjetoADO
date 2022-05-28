using ADO.Business.Interfaces;
using ADO.Business.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Data.Repository
{
    public class CursoRepository : BaseADO, ICursoRepository
    {
        public CursoRepository(IConfiguration configure,
                                INotificador notificador) :
            base(configure, notificador){ }

        public Task<Curso> Adicionar(Curso entity)
        {
            throw new NotImplementedException();
        }

        public Task<Curso> Atualizar(Curso entity)
        {
            throw new NotImplementedException();
        }

        public Task<Curso> ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Curso>> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public Task Remover(int id)
        {
            throw new NotImplementedException();
        }
    }
}
