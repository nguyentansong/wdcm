using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WDCM_Api.Entities;

namespace WDCM_Api.Data
{
    public class WDCMDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<PdfFiles> PdfFiles { get; set; }
        public WDCMDbContext()
        {

        }

        public WDCMDbContext(DbContextOptions<WDCMDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>((option)=> {
                option.ToTable("User");
                option.Ignore("Password");
            });
            modelBuilder.Entity<Menu>().ToTable("Menu");
            modelBuilder.Entity<PdfFiles>().ToTable("PdfFiles");
        }
    }
}
