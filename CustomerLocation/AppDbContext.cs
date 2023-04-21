using Customer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Customer
{


    public class AppDbContext : DbContext
    {
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<Location> Locations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();

                string connectionString = configuration.GetConnectionString("DevConnection");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Customer entity
            modelBuilder.Entity<CustomerModel>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Add new property for the PhoneNumber column
            /*
            modelBuilder.Entity<CustomerModel>()
                .Property(c => c.PhoneNumber)
                .HasMaxLength(20);*/


        
     
      

    }
    }
}
