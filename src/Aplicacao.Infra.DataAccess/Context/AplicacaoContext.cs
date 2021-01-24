using Aplicacao.Domain.Model;
using Aplicacao.Infra.DataAccess.Map;
using Aplicacao.Infra.DataAccess.Seed;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AplicacaoContext).Assembly);
            MapearPropriedadesEsquecidas(modelBuilder);

            //base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);

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

        private void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
        {
            foreach (var item in modelBuilder.Model.GetEntityTypes())
            {
                var prop = item.GetProperties().Where(c => c.ClrType == typeof(string));

                foreach (var itemProp in prop)
                {
                    if (string.IsNullOrEmpty(itemProp.GetColumnType())
                        && !itemProp.GetMaxLength().HasValue)
                    {
                        //itemProp.SetMaxLength(100);
                        itemProp.SetColumnType("VARCHAR(100)");
                    }
                }
            }
        }
    }
}