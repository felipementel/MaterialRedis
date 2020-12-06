using Aplicacao.Domain.Model;
using Aplicacao.Infra.DataAccess.Map;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Infra.DataAccess.Context
{
    public class AplicacaoContext : DbContext
    {
        public AplicacaoContext(DbContextOptions<AplicacaoContext> options)
           : base(options)
        {

        }

        public DbSet<Customer> Clientes { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationFailure>();
            modelBuilder.Ignore<ValidationResult>();

            //Map
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new AddressMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new OrderItemsMap());

            //Seed
            //modelBuilder.ApplyConfiguration(new ProductSeed());

            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //if (!optionsBuilder.IsConfigured)
            //{                
            //    IConfigurationRoot configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory()) //Microsoft.Extensions.Configuration.FileExtensions
            //    .AddJsonFile("appsettings.json") //Microsoft.Extensions.Configuration.Json
            //    .Build();

            //    var connectionString = configuration.GetConnectionString("Aplicacao");
            //    optionsBuilder.UseSqlServer(connectionString, x => x.EnableRetryOnFailure())
            //    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            //}
        }
    }
}