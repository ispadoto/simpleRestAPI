using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using SimpleRestAPI.Domain.Entities.Employees;
using SimpleRestAPI.Domain.Entities.EmployeesPhones;

namespace SimpleRestAPI.Infra.Database.Context
{
	public class SimpleRestDB : DbContext
    {
        public SimpleRestDB(DbContextOptions<SimpleRestDB> options) : base(options)
        {

        }

        #region DBSets
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeePhone> EmployeePhone { get; set; }

        #endregion

        #region Methods Overrides

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // defaults for all models

            // decimal fields (if not manually setup)
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                    .SelectMany(t => t.GetProperties())
                    .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                if (property.GetColumnType() == null)
                    property.SetColumnType("decimal(13,4)");
            }

            // all string fields size
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                    .SelectMany(t => t.GetProperties())
                    .Where(p => p.ClrType == typeof(string)))
            {
                if (property.GetColumnType() == null)
                    property.SetColumnType("varchar(300)");
            }

            // phone fields size
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                    .SelectMany(t => t.GetProperties())
                    .Where(p => p.ClrType == typeof(string) && p.PropertyInfo.Name.Contains("Phone")))
            {
                if (property.GetColumnType() == null)
                    property.SetColumnType("varchar(20)");
            }

            //customs 
            //modelBuilder.ApplyConfiguration(new UserConfiguration());

        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Property("Id").CurrentValue == null) entry.Property("Id").CurrentValue = Guid.NewGuid();
                    if (entry.Property("Active").CurrentValue == null) entry.Property("Active").CurrentValue = true;
                    if (entry.Property("Deleted").CurrentValue == null) entry.Property("Deleted").CurrentValue = false;

                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified) entry.Property("CreatedAt").IsModified = false;
            }
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("UpdatedAt") != null))
            {
                if (entry.State == EntityState.Modified)
                    entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
            }

            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}