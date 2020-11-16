
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;


namespace SIFCore.Models
{
     public class SIFContext : DbContext
    {
        public SIFContext (DbContextOptions<SIFContext> options)
            : base(options)
        {
        }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<Analysis> Analysis { get; set; }

        public DbSet<AnalysisTypes> AnalysisTypes { get; set; }

        public DbSet<BillingAddresses> BillingAddresses { get; set; }

        public DbSet<Contacts> Contacts { get; set; }

        public DbSet<Requirements> Requirements { get; set; }

        public DbSet<ShippingAddresses> ShippingAddresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=cherry01;Database=StableIsotopeFacility-Dev;Trusted_Connection=True;");
            }
            //optionsBuilder.UseLoggerFactory(GetLoggerFactory());

        }
    }
}