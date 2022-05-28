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
    public class AlunoService : BaseService, IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoService(IAlunoRepository alunoRepository,
                            IConfiguration configure,
                            INotificador notificador)
                            : base(configure, notificador)
        {
            _alunoRepository = alunoRepository;

        }

        public async Task<Aluno> Adicionar(Aluno aluno)
        {
            using (var conn = Connection)
            {

                var query = $"SELECT * FROM alunos AS A WHERE A.CPF={@aluno.CPF}";
                var result = await conn.ExecuteAsync(query, aluno);
                if (result > 0)
                {
                    AdicionarNotificacoes("Já existe um Aluno com esse Documento ");
                    return aluno;
                }
                await _alunoRepository.Adicionar(aluno);
                return aluno;
            }

        }

        public async Task<Aluno> Atualizar(Aluno aluno)
        {
           return await _alunoRepository.Atualizar(aluno);
            

        }

        public async Task Excluir(int id)
        {
           await _alunoRepository.Remover(id);
        }
    }
}
