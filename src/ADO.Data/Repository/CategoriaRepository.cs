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
    public class CategoriaRepository : BaseADO, ICategoriaRepository
    {
        public CategoriaRepository(IConfiguration configuration,
            INotificador notificador):base(configuration, notificador) { }
     


    public async Task<Categoria> Adicionar(Categoria categoria)
        {
            string query = $"INSERT INTO categorias (descricao, cadastrado_em)" +
                " VALUES ( @descricao, @cadastrado_em)";

            try
            {
                using (var conn = Connection)
                {
                    return await conn.ExecuteScalarAsync<Categoria>(query, categoria);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Categoria> Atualizar(Categoria categoria)
        {
            string query = $"UPDATE categorias SET descricao= @descricao WHERE id = {@categoria.Id}";
            try
            {
                using (var conn = Connection)
                {
                    return await conn.ExecuteScalarAsync<Categoria>(query, categoria);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Categoria> ObterPorId(int id)
        {

            string query = $"SELECT * FROM Categorias as a WHERE a.id = {@id}";

            try
            {
                using (var conn = Connection)
                {
                   var result= await conn.QueryFirstAsync<Categoria>(query, id);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;

            }
           
        }

        public async Task<IEnumerable<Categoria>> ObterTodos()
        {
            using (var conn = Connection)
            {
                //pega o nome da classe Repository e fica só com o nome sem o REPOSITORY

                string query = $"SELECT * FROM categorias";
                return await conn.QueryAsync<Categoria>(query);
            }
        }

        public async Task Remover(int id)
        {
            string query = $"DELETE FROM Categorias WHERE id = {@id}";

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
