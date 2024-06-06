using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Domain.Context
{
    public partial class ExecContext : DbContext
    {
        public bool DisableCache { get; set; }

        public ExecContext()
        {
        }

        public ExecContext(DbContextOptions<ExecContext> options)
            : base(options)
        {
            this.DisableCache = false;
        }

        public ExecContext(DbContextOptions<ExecContext> options, ExecContextOption execContextOption)
            : base(options)
        {
            if (execContextOption != null)
                this.DisableCache = execContextOption.DisableCache;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.1.1");
        }
    }
}