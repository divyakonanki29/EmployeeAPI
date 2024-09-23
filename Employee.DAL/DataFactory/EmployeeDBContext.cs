using Employee.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.DAL.DataFactory
{
    public class EmployeeDBContext: DbContext
    {
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options): base(options)
        {
            
        }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<EmployeeSalaryComponentEntity> EmployeeSalaryComponents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // relationship of employee and address
            modelBuilder.Entity<EmployeeEntity>()
                    .HasOne(x=>x.Address)
                    .WithOne(x=>x.employeeEntity)
                    .HasForeignKey<AddressEntity>(x=>x.EmployeeId);


            // relationship of employee and EmployeeSalaryComponents
            modelBuilder.Entity<EmployeeEntity>()
                    .HasOne(x => x.EmployeeSalaryComponent)
                    .WithOne(x => x.employeeEntity)
                    .HasForeignKey<EmployeeSalaryComponentEntity>(x => x.EmployeeId);
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is EmployeeEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((EmployeeEntity)entry.Entity).CreatedOn = DateTime.UtcNow;
                }

                ((EmployeeEntity)entry.Entity).UpdatedOn = DateTime.UtcNow;
            }

            return await base.SaveChangesAsync();
        }
    }
}
