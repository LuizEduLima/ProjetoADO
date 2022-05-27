using ADO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADO.Web.Mappings
{
    public class AlunoMapping : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.Property(c => c.Nome)
                 .IsRequired()
                 .HasColumnType("varchar(80)");

            builder.Property(c => c.CPF)
                .IsRequired()
                .HasColumnType("char(11)");

            builder.HasIndex(c => c.CPF).IsUnique();
                

            builder.Property(c => c.Cidade)
                .IsRequired()
                .HasColumnType("varchar(60)");

            builder.Property(c => c.Estado)
                .IsRequired()
                .HasColumnType("varchar(2)");

            builder.Property(c => c.Nascimento)
                .IsRequired();

            builder.HasKey(c => c.Id);

            builder.ToTable("Alunos");
                



        }
    }
}
