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

        public DbSet<ADO.Business.Models.Curso> Curso { get; set; }

        public DbSet<ADO.Business.Models.Categoria> Categoria { get; set; }
    }
}
