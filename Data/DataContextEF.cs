using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityFramework.Data
{
    public class DataContextEF : DbContext
    {
       private IConfiguration _config;

      //  private string? _connectionString;
        public DataContextEF(IConfiguration config)
        {
             _config = config;
           
        }
        public DbSet<Computer>? Computer {get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if(!options.IsConfigured)
            {
                // options.UseSqlServer("Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;"
                // ,options => options.EnableRetryOnFailure());
               options.UseSqlServer(_config.GetConnectionString("DefaultConnection")
                , options => options.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            modelBuilder.HasDefaultSchema("TutorialAppSchema");
            modelBuilder.Entity<Computer>()
            //.HasNoKey()
            .HasKey( c => c.ComputerId);
            //.ToTable("Computer", "TutorialAppSchema");
            //.ToTable("TableName", "SchemaName");
        }




    }

}
