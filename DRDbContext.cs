using DRAssessment.Models;
using Microsoft.EntityFrameworkCore;
//using MySql.EntityFrameworkCore.Extensions;
using Pomelo.EntityFrameworkCore.MySql;

namespace DRAssessment
{
    public class DRDbContext : DbContext
    {
        public DRDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<VacationRequest> VacationRequests { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ApprovalHistory> ApprovalHistories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Operation> Operations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            String connectString = "server=localhost;port=3306;database=drdb;user=root";
            optionsBuilder.UseMySql(connectString,
                        new MySqlServerVersion(new Version(8, 0, 25)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.Head)
                .WithOne()
                .HasForeignKey<Department>(d => d.HeadId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VacationRequest>()
                .HasOne(v => v.Employee)
                .WithMany(e => e.VacationRequests)
                .HasForeignKey(v => v.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApprovalHistory>()
                .HasOne(a => a.VacationRequest)
                .WithMany(v => v.ApprovalHistories)
                .HasForeignKey(a => a.VacationRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApprovalHistory>()
            .HasOne(a => a.HeadApprover)
            .WithMany()
            .HasForeignKey(a => a.HeadApproverId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApprovalHistory>()
            .HasOne(a => a.Approver)
            .WithMany()
            .HasForeignKey(a => a.ApproverId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
