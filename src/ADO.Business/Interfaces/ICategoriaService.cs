using ADO.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Business.Interfaces
{
    public interface ICategoriaService
    {
        Task<Categoria> Adicionar(Categoria categoria);
        Task<Categoria> Atualizar(Categoria categoria);
        Task Excluir(int id);
    }
}
