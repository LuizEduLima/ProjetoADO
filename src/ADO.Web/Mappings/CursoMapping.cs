using ADO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADO.Web.Mappings
{
    public class CursoMapping : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descricao)
                 .IsRequired()
                 .HasColumnType("VARCHAR(80)");

            builder.HasIndex(c => c.Descricao).IsUnique();

            builder.Property(c => c.TotalHoras)
                .IsRequired();

            builder.Property(c => c.Valor)
             .IsRequired()
             .HasColumnType("DECIMAL(12,2)");


        

            //EF 1:N
            builder.HasOne(c => c.Categoria)
                .WithMany(cat => cat.cursos)
                .HasForeignKey(c => c.CategoriaId);

            builder.ToTable("Cursos");






        }
    }
}
