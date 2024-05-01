using DatabaseProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProject.DatabaseContext
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Panel> Panel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Panel>().HasKey(p => p.id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
