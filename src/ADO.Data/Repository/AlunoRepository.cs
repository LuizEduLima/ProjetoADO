using ADO.Business.Interfaces;
using ADO.Business.Models;
using ADO.Data.Exceptions;
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
            string query = $"SELECT * FROM alunos as a WHERE a.id = {@id}";
            var Result = new Aluno { Id = id };
            try
            {
                using (var conn = Connection)
                {
                 /* await conn.ExecuteAsync(sql: query, param: new { id })*/;
                    return await conn.QueryFirstAsync<Aluno>(query, id);
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
        public async Task<Aluno> Atualizar(Aluno aluno)
        {
            string query = "UPDATE alunos SET nome= @nome, descricao=@descricao, " +
               "CPF=@CPF, cidade= @cidade, estado= @estado " +
               $"WHERE id ={@aluno.Id}";
            try
            {
                using (var conn = Connection)
                {
                    return await conn.ExecuteScalarAsync<Aluno>(query, aluno);
                 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }    
        public async Task Remover(int id)
        {
            string query = $"DELETE FROM alunos WHERE id = {@id}";

            try
            {
                using (var conn = Connection)
                {
                    await conn.ExecuteAsync(sql: query, param: new { id });
                }

            }
            catch (Exception ex)
            {
                throw new DomainException("Ops! Não foi possível excluir esse registro porque" +
                    " ele está associdado a outras tabelas.");
            }
        }
    }
}
