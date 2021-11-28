using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Context
{
    public class CleanArchitectureDbContext : DbContext
    {
        public CleanArchitectureDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanArchitectureDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Pre-convention model configuration 
        /// https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/whatsnew
        /// </summary>
        /// <param name="configurationBuilder"></param>
        //protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        //{
        //    configurationBuilder.Properties<string>().HaveMaxLength(200);
        //}
    }
}