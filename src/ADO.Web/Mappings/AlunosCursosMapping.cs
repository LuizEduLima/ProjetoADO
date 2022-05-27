using ADO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADO.Web.Mappings
{
    public class AlunosCursosMapping : IEntityTypeConfiguration<AlunoCurso>
    {
        public void Configure(EntityTypeBuilder<AlunoCurso> builder)
        {
            builder.HasKey(a => new {a.Aluno_id,a.Curso_Id });
        }
    }
}
