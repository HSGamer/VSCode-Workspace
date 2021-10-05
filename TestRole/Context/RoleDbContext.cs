using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestRole.Models;

namespace TestRole.Context
{
    public class RoleDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }

        public RoleDbContext(DbContextOptions<RoleDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Account>().HasKey(a => a.AccountId);

            builder.Entity<Student>().HasKey(s => s.StudentId);
            builder.Entity<Student>()
                    .HasOne(s => s.Account)
                    .WithOne(a => a.Student)
                    .HasForeignKey<Student>(s => s.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Trainer>().HasKey(t => t.TrainerId);
            builder.Entity<Trainer>()
                    .HasOne(t => t.Account)
                    .WithOne(a => a.Trainer)
                    .HasForeignKey<Trainer>(t => t.TrainerId)
                    .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Classroom>().HasKey(c => c.ClassId);
            builder.Entity<Classroom>()
                    .HasOne(c => c.Trainer)
                    .WithMany(t => t.Classrooms)
                    .HasForeignKey(c => c.TrainerId)
                    .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Student>()
                    .HasOne(s => s.Classroom)
                    .WithMany(c => c.Students)
                    .HasForeignKey(s => s.ClassId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}