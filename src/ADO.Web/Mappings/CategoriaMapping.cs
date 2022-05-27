using ADO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADO.Web.Mappings
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("VARCHAR(80)");           

            builder.HasKey(c => c.Id);

            builder.ToTable("Categorias");


        }
    }
}
