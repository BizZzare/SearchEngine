using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchEngine.Core.Models;
namespace SearchEngine.Config
{
    public class SearchEngineDbContext : DbContext
    {
        public SearchEngineDbContext() : base("SearchEngineDb")
        {

        }
        public DbSet<Query> Queries { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Query>()
                .HasMany(s => s.Results)
                .WithRequired(e => e.Query)
                .HasForeignKey(e => e.QueryId);
            
        }
    }

}
