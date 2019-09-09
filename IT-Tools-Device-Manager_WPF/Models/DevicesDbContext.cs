using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ITTools_DeviceManager_WPF.Models
{
    public partial class DevicesDbContext : DbContext
    {
        private string dbDatasourcePath = @"Datasource=C:\Users\11014215\Documents\!PROJECTS\WPF\Device Manager\deviceDatabase.db";
        public DevicesDbContext()
        {
        }

        public DevicesDbContext(DbContextOptions<DevicesDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Device> Device { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(dbDatasourcePath);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Device>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedNever();
            });
        }
    }
}
