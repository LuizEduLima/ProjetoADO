using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ADO.Business.Models;

namespace ADO.Web.Data
{
    public class ADOWebContext : DbContext
    {
        public ADOWebContext (DbContextOptions<ADOWebContext> options)
            : base(options)
        {
        }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<AlunoCurso> AlunosCursos { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(p=>p.GetProperties()
                .Where(p=>p.ClrType==typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ADOWebContext).Assembly);
        }
    }

    
}
