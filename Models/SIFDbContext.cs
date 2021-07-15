
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;


namespace SIFCore.Models
{
     public class SIFContext : DbContext
    {       

        public DbSet<Orders> Orders { get; set; }

        public DbSet<Analysis> Analysis { get; set; }

        public DbSet<AnalysisTypes> AnalysisTypes { get; set; }

        public DbSet<BillingAddresses> BillingAddresses { get; set; }

        public DbSet<Contacts> Contacts { get; set; }

        public DbSet<Employees> Employees { get; set; }

        public DbSet<Requirements> Requirements { get; set; }

        public DbSet<ShippingAddresses> ShippingAddresses { get; set; }

        public DbSet<Customers> Customers { get; set; }

        public DbSet<Charges> Charges { get; set; }

        public DbSet<Ancillary> Ancillary { get; set; }

        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                   builder.AddConsole()
                          .AddFilter(DbLoggerCategory.Database.Command.Name,
                                     LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }

        public SIFContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SIFCoreContext"));
            }
            optionsBuilder.UseLoggerFactory(GetLoggerFactory());

        }
    }
}