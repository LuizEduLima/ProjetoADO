using ADO.Business.Interfaces;
using ADO.Business.Models;
using ADO.Data.Data;
using ADO.Data.Exceptions;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Data.Repository
{
    public class CategoriaRepository : BaseADO, ICategoriaRepository
    {
        public CategoriaRepository(IConfiguration configuration,
            INotificador notificador, ADOWebContext _context) : 
            base(configuration, notificador, _context) { }



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
                    var result = await conn.QueryFirstAsync<Categoria>(query, id);
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
            //SELECT * FROM cursos as c left join Categorias as cat on c.CategoriaId = c.Id 

            string queryConfirme = $"SELECT * FROM cursos WHERE categoriaId = {@id}";
            string query = $"DELETE FROM Categorias WHERE id = {@id}";


            try
            {
                using (var conn = Connection)
                {
                    IEnumerable<Curso> result = await conn.QueryAsync<Curso>(sql: queryConfirme, param: new { id });

                    if (result.Count() > 0)
                    {
                        AdicionarErroProcessamento("Esse registro não pode ser excluído");
                        return;
                    }

                    await conn.ExecuteAsync(sql: query, param: new { id });
                }

            }
            catch (Exception ex)
            {

                AdicionarErroProcessamento("Erro ao Excluir cadastro...");
            }
        }



    }

}

