using ADO.Business.Interfaces;
using ADO.Business.Models;
using ADO.Data.Exceptions;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ADO.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly BaseADO _base;
        public Repository(BaseADO baseADO)
        {
            _base = baseADO;
        }
        public async Task<IEnumerable<T>> ObterTodos()
        {

            try
            {
                using (var conn = _base.Connection)
                {
                    //pega o nome da classe Repository e fica só com o nome sem o REPOSITORY
                    var nomeTabela = GetType().Name.Split("Repository");
                    string query = $"SELECT * FROM {nomeTabela[0]}s";
                    return await conn.QueryAsync<T>(query);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> Remover(int id)
        {
            var nomeTabela = GetType().Name.Split("Repository");
            string query = $"DELETE FROM {nomeTabela[0]}s WHERE id = @id";

            try
            {
                using (var conn = _base.Connection)
                {
                    return await conn.ExecuteAsync(sql: query, param: new { id });
                }

            }
            catch (Exception )
            {
                throw new DomainException("Ops! Não foi possível excluir esse registro porque" +
                    " ele está associdado a outras tabelas.");
            }
        }

        public async Task<int> Adicionar(T entity)
        {
            var nomeTabela = GetType().Name.Split("Repository");
            string query = $"INSERT INTO {nomeTabela[0]}s (nome,cpf,cidade,estado,nascimento,cadastrado_em)" +
                " VALUES ( @nome, @CPF, @cidade, @estado,@nascimento,@cadastrado_em)";

            try
            {
                using (var conn = _base.Connection)
                {
                    return await conn.ExecuteAsync(sql: query, param: entity); 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> Atualizar(T entity)
        {
            var nomeTabela = GetType().Name.Split("Repository");
            string query = $"UPDATE {nomeTabela[0]}s SET nome= @nome, descricao=@descricao, " +
                "CPF=@CPF, cidade= @cidade, estado= @estado " +
                "WHERE id = @id";
            try
            {
                using (var conn = _base.Connection)
                {
                    await conn.ExecuteAsync(sql: query, param: entity);
                    return entity;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> ObterPorId(int id)
        {
            var nomeTabela = GetType().Name.Split("Repository");
            string query = $"SELECT * FROM {nomeTabela[0]}s where id = @id";

            try
            {
                using (var conn = _base.Connection)
                {
                    return await conn.QueryFirstOrDefaultAsync<T>(sql:query, param:new { id});
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
