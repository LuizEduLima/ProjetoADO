using ADO.Business.Interfaces;
using ADO.Business.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace ADO.Business.Services
{
    public class CategoriaService : BaseService, ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository,
                            IConfiguration configure,
                            INotificador notificador)
                            : base(configure, notificador)
        {
            _categoriaRepository = categoriaRepository;

        }

        public async Task<Categoria> Adicionar(Categoria categoria)
        {
            using (var conn = Connection)
            {

                var query = $"SELECT * FROM categorias AS C WHERE C.descricao ='{@categoria.Descricao}'";
                var result = await conn.ExecuteAsync(query, categoria);
                if (result > 0)
                {
                    AdicionarNotificacoes("Já existe um Aluno com esse Documento ");
                    return categoria;
                }
                return await _categoriaRepository.Adicionar(categoria);
            }
        }

        public async Task<Categoria> Atualizar(Categoria categoria)
        {
           return await _categoriaRepository.Atualizar(categoria);
            

        }

        public async Task Excluir(int id)
        {
            
           await _categoriaRepository.Remover(id);
        }
    }
}
