using MatterWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MatterWebAPI
{
    public class AppDbContext : DbContext
    {
        public DbSet<Attorney> Attorneys { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Jurisdiction> Jurisdictions { get; set; }
        public DbSet<Matter> Matters { get; set; }
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
            // Configure the one-to-many Attorney-Jurisdiction relationship
            modelBuilder.Entity<Attorney>()
                .HasOne(a => a.Jurisdiction) // Each Attorney has one Jurisdiction
                .WithMany(j => j.Attorneys) // Each Jurisdiction has many Attorneys
                .HasForeignKey(a => a.JurisdictionId) // Foreign key for the Jurisdiction navigation property on the Attorney class
                .IsRequired(); // The Jurisdiction foreign key is required

            // Configure the one-to-many Matter-Jurisdiction relationship
            modelBuilder.Entity<Matter>()
                .HasOne(m => m.Jurisdiction) // Each Matter has one Jurisdiction
                .WithMany(j => j.Matters) // Each Jurisdiction has many Matters
                .HasForeignKey(m => m.JurisdictionId) // Foreign key for the Jurisdiction navigation property on the Matter class
                .IsRequired(); // The Jurisdiction foreign key is required

            // Configure the many-to-one Matter-BillingAttorney relationship
            modelBuilder.Entity<Matter>()
                .HasOne(m => m.BillingAttorney) // Each Matter has one Billing Attorney
                .WithMany() // Each Billing Attorney can be associated with many Matters
                .HasForeignKey(m => m.BillingAttorneyId) // Foreign key for the Billing Attorney navigation property on the Matter class
                .IsRequired() // The Billing Attorney foreign key is required
                .OnDelete(DeleteBehavior.Restrict); // Add this line 

            // Configure the many-to-one Matter-ResponsibleAttorney relationship
            modelBuilder.Entity<Matter>()
                .HasOne(m => m.ResponsibleAttorney) // Each Matter has one Responsible Attorney
                .WithMany() // Each Responsible Attorney can be associated with many Matters
                .HasForeignKey(m => m.ResponsibleAttorneyId) // Foreign key for the Responsible Attorney navigation property on the Matter class
                .IsRequired() // The Responsible Attorney foreign key is required
                .OnDelete(DeleteBehavior.Restrict); // Add this line 


            // Configure the many-to-one Matter-Client relationship
            modelBuilder.Entity<Matter>()
                .HasOne(m => m.Client) // Each Matter has one Client
                .WithMany(c => c.Matters) // Each Client can be associated with many Matters
                .HasForeignKey(m => m.ClientId) // Foreign key for the Client navigation property on the Matter class
                .IsRequired(); // The Client foreign key is required

            // Disable cascade delete behavior for all foreign key relationships
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Matter)
                .WithMany(m => m.Invoices)
                .HasForeignKey(i => i.MatterId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Attorney)
                .WithMany()
                .HasForeignKey(i => i.AttorneyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
        }
    }
    }

