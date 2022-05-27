using ADO.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Business.Interfaces
{
    public interface IRepository<T> where T: Entity
    {

        Task<IEnumerable<T>> ObterTodos();
        Task<T> ObterPorId(int id);
        Task<T> Adicionar(T entity);
        Task<T> Atualizar(T entity);
        Task Remover(int id);


    }
}
