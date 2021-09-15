using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PLS.Application.Common.Interfaces;
using PLS.Domain;

namespace PLS.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(
       DbContextOptions options) : base(options)
        {
            var connectionString = Database.GetDbConnection().ConnectionString;
        }

        public DbSet<Spot> Spots { get; set; }
        public DbSet<Level> Levels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test");
        //}
    }
}
