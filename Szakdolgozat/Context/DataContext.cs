using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Szakdolgozat.Models;

namespace Szakdolgozat.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options):base(options)
        {
        }
        public DbSet<Models.BusinessModel> Business { get; set; }
        public DbSet<Models.CateringModel> Catering { get; set; }
        public DbSet<Models.EmployeeModel> Employee { get; set; }
        public DbSet<Models.GDPModel> GDP { get; set; }
        public DbSet<Models.IndustryModel> Industry { get; set; }
        public DbSet<Models.PriceModel> Price { get; set; }
        public DbSet<Models.TourismModel> Tourism { get; set; }
        public DbSet<Models.UnemployedModel> Unemployed { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.BusinessModel>().ToTable("Business");
            modelBuilder.Entity<Models.CateringModel>().ToTable("Catering");
            modelBuilder.Entity<Models.EmployeeModel>().ToTable("Employee");
            modelBuilder.Entity<Models.GDPModel>().ToTable("GDP");
            modelBuilder.Entity<Models.IndustryModel>().ToTable("Industry");
            modelBuilder.Entity<Models.PriceModel>().ToTable("Price");
            modelBuilder.Entity<Models.TourismModel>().ToTable("Tourism");
            modelBuilder.Entity<Models.UnemployedModel>().ToTable("Unemployed");
        }

        
    }
}
