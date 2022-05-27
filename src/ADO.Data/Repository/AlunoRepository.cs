using ADO.Business.Interfaces;
using ADO.Business.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Data.Repository
{
    public class AlunoRepository :BaseADO, IAlunoRepository
    {

        public AlunoRepository(IConfiguration configure) : base(configure) { }

        public async Task<IEnumerable<Aluno>> ObterTodos()
        {
            using (var conn = Connection)
            {
                //pega o nome da classe Repository e fica só com o nome sem o REPOSITORY
                
                string query = $"SELECT * FROM alunos";
                return await conn.QueryAsync<Aluno>(query);
            }
        }
        public async Task<Aluno> ObterPorId(int id)
        {
            string query = $"SEÇECT * FROM alunos WHERE id = @id";
            var Result = new Aluno { Id = id };
            try
            {
                using (var conn = Connection)
                {
                 /* await conn.ExecuteAsync(sql: query, param: new { id })*/;
                  Result= await conn.QueryFirstAsync<Aluno>(query, id);
                }

            }
            catch(Exception ex)
            {
                
            }
            return Result;
        }
        public async Task<Aluno> Adicionar(Aluno aluno)
        {
           
            string query = $"INSERT INTO alunos (nome,cpf,cidade,estado,nascimento,cadastrado_em)" +
                " VALUES ( @nome, @CPF, @cidade, @estado,@nascimento,@cadastrado_em)";

            try
            {
                using (var conn =Connection)
                {
                    return await conn.ExecuteScalarAsync<Aluno>(query, aluno);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Aluno> Atualizar(Aluno entity)
        {
            throw new NotImplementedException();
        }

       

       

        public Task Remover(int id)
        {
            throw new NotImplementedException();
        }
    }
}
