using ADO.Business.Interfaces;
using ADO.Business.Models;
using ADO.Business.Notificacoes;
using ADO.Data.Data;
using ADO.Data.Exceptions;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Data.Repository
{
    public class AlunoRepository : BaseADO, IAlunoRepository
    {


        public AlunoRepository(IConfiguration configuration,
                                INotificador notificador, 
                                ADOWebContext _context) :
            base(configuration, notificador, _context)
        { }
     

        public async Task<IEnumerable<Aluno>> ObterTodos()
        {
            using (var conn = Connection)
            {

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
                    var result = await conn.ExecuteReaderAsync(query);
                    if (!result.HasRows)
                    {
                        AdicionarErroProcessamento("Aluno não encontrado..");
                        return null;
                    }

                    return await conn.QueryFirstAsync<Aluno>(query, id);
                }
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento("Erro inesperado :(");
            }
            return Result;
        }
        public async Task<Aluno> Adicionar(Aluno aluno)
        {

            string query = $"INSERT INTO alunos (nome,cpf,cidade,estado,nascimento,cadastrado_em)" +
                " VALUES ( @nome, @CPF, @cidade, @estado,@nascimento,@cadastrado_em)";

            try
            {
                using (var conn = Connection)
                {
                    return await conn.QueryFirstOrDefaultAsync<Aluno>(query, aluno);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Remover(int id)
        {
            //id = 123;
            string query = $"DELETE FROM alunos WHERE id = {@id}";
           
            try
            {
                await Connection.ExecuteAsync(query, new { Id = id });

            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento("Ops! Erro inesperado: ");
               
            }
        }
        public async Task<bool>ObterBool(int id)
        {
            return await Connection.QueryFirstOrDefaultAsync<bool>(@$"Select * from Alunos where id = {id}");
        }
    }
}
