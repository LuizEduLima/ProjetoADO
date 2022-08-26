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
    public class CursoService : BaseService, ICursoService
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoService(ICursoRepository cursoRepository,
                            IConfiguration configure,
                            INotificador notificador)
                            : base(configure, notificador)
        {
            _cursoRepository = cursoRepository;

        }

        public async Task<Curso> Adicionar(Curso curso)
        {
            using (var conn = Connection)
            {
                var query = $"select * from cursos where Descricao = '{@curso.Descricao}'";
                var result = await conn.ExecuteAsync(query, curso);
                if (result > 0)
                {
                    AdicionarNotificacoes("Já existe um curso com essa Descrição ");
                    return curso;
                }
                return await _cursoRepository.Adicionar(curso);
               
            }

        }

        public async Task<Curso> Atualizar(Curso curso)
        {
           return await _cursoRepository.Atualizar(curso);
            

        }

        public async Task Excluir(int id)
        {
           await _cursoRepository.Remover(id);
        }
    }
}
