using ADO.Business.Interfaces;
using ADO.Business.Models;
using ADO.Data.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Data.Repository
{
    public class CursoRepository : BaseADO, ICursoRepository
    {
        public CursoRepository(IConfiguration configuration,
                                INotificador notificador, ADOWebContext _context) :
            base(configuration, notificador, _context)
        { }
      

        public async Task<Curso> Adicionar(Curso curso)
        {
            string query = $"INSERT INTO cursos (descricao,TotalHoras,Valor,Ativo,CategoriaId,cadastrado_em)" +
                " VALUES ( @descricao, @TotalHoras, @Valor, @Ativo,@CategoriaId,@cadastrado_em)";

            try
            {
                using (var conn = Connection)
                {
                    return await conn.ExecuteScalarAsync<Curso>(query, curso);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Curso> Atualizar(Curso curso)
        {
            string query = "UPDATE cursos SET Descricao = @Descricao,  " +
                           "TotalHoras=@TotalHoras, Valor= @Valor, " +
                           "Ativo= @Ativo, CategoriaId = @CategoriaId " +
                          $"WHERE id = {@curso.Id}";
            try
            {
                using (var conn = Connection)
                {
                    var result = await conn.ExecuteAsync(query, curso);
                    if (result < 0)
                    {
                        AdicionarErroProcessamento("Houve um erro inesperado! Tente de novo mais tarde...");
                        return curso;
                    }
                    return curso;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task MatricularAlunoCurso(int alunoId, int cursoId)
        {

            var result = await Connection.QueryFirstOrDefaultAsync<AlunoCurso>("SELECT * FROM AlunosCursos WHERE Aluno_Id = @a_Id AND Curso_Id = @c_Id", new { a_Id = alunoId, c_Id = cursoId });
               


            if (result != null)
            {
                AdicionarErroProcessamento($"Ops! O Aluno Já Matrículado no Curso!");

            }
            else
            {
                string query = "INSERT INTO AlunosCursos (Aluno_Id, Curso_id)VALUES (@Aluno_Id, @Curso_id)";
                await Connection.QueryAsync(query, new { Aluno_id = alunoId, Curso_Id = cursoId });
            }          

        }

        public async Task<IEnumerable<Curso>> ObterAlunosPorCursos()
        {
            var listaCursos = new List<Curso>();

            string query = "SELECT * FROM Cursos AS C LEFT JOIN Categorias AS CT on C.CategoriaId = CT.Id INNER JOIN " +
                "AlunosCursos AS AC ON AC.Curso_Id = C.Id LEFT JOIN Alunos AS A ON AC.Aluno_id = A.Id";

            await Connection.QueryAsync<Curso, Categoria, Aluno, Curso>(query,
                (curso, categoria, aluno) =>
                {
                    //verificar se o curso tem na lista pra retirar a repetição!
                    if (listaCursos.SingleOrDefault(c => c.Id == curso.Id) == null)
                    {
                        curso.Alunos = new List<Aluno>();
                        curso.Categoria = categoria;
                        listaCursos.Add(curso);
                    }
                    else
                    {
                        curso = listaCursos.SingleOrDefault(c => c.Id == curso.Id);
                    }
                    curso.Alunos.Add(aluno);
                    return curso;
                }, splitOn: "Id, Id, Id ");

            return listaCursos;

        }

        public async Task<Curso> ObterPorId(int id)
        {
            string query = $"SELECT * FROM cursos as a WHERE a.id = {@id}";

            try
            {
                using (var conn = Connection)
                {
                    return await conn.QueryFirstAsync<Curso>(query, id);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<IEnumerable<Curso>> ObterTodos()
        {
            string query = "SELECT * FROM cursos as c INNER JOIN categorias as cg ON c.CategoriaId = cg.Id";
            using (var conn = Connection)
            {
                return await conn.QueryAsync<Curso, Categoria, Curso>(query,
                    map: (curso, categoria) =>
                     {
                         curso.Categoria = categoria;
                         return curso;
                     }, splitOn: "Id,Id");
            }
        }


        public async Task Remover(int id)
        {
            string query = $"DELETE FROM cursos WHERE id = {@id}";

            try
            {
                using (var conn = Connection)
                {
                    await conn.ExecuteAsync(sql: query, param: new { id });
                }

            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento("Não foi possível Excluir esse Registro!");
            }
        }
    }
}
