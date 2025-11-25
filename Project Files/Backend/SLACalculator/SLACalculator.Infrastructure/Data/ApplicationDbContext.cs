using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SLACalculator.Application.Common.Interfaces;
using SLACalculator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SLACalculator.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<WorkingDay> WorkingDays => Set<WorkingDay>();
        public DbSet<WorkingHours> WorkingHours => Set<WorkingHours>();
        public DbSet<BusinessClosure> BusinessClosures => Set<BusinessClosure>();
        public DbSet<SlaConfiguration> SlaConfigurations => Set<SlaConfiguration>();
        public DbSet<Complaint> Complaints => Set<Complaint>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WorkingDay>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DayOfWeek).IsRequired();
            });

            modelBuilder.Entity<WorkingHours>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<BusinessClosure>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<SlaConfiguration>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Complaint>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasQueryFilter(c => !c.IsDeleted);
            });

            //Seed Test data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkingDay>().HasData(
                new WorkingDay { Id = 1, DayOfWeek = DayOfWeek.Sunday, IsWorkingDay = true },
                new WorkingDay { Id = 2, DayOfWeek = DayOfWeek.Monday, IsWorkingDay = true },
                new WorkingDay { Id = 3, DayOfWeek = DayOfWeek.Tuesday, IsWorkingDay = true },
                new WorkingDay { Id = 4, DayOfWeek = DayOfWeek.Wednesday, IsWorkingDay = true },
                new WorkingDay { Id = 5, DayOfWeek = DayOfWeek.Thursday, IsWorkingDay = true },
                new WorkingDay { Id = 6, DayOfWeek = DayOfWeek.Friday, IsWorkingDay = false },
                new WorkingDay { Id = 7, DayOfWeek = DayOfWeek.Saturday, IsWorkingDay = false }
            );

            modelBuilder.Entity<WorkingHours>().HasData(
                new WorkingHours
                {
                    Id = 1,
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(17, 0, 0),
                    Description = "Everyday Work Hours form 8 AM to 5 PM"
                }
            );

            modelBuilder.Entity<SlaConfiguration>().HasData(
                new SlaConfiguration { Id = 1, Priority = Domain.Enums.Priority.High, ResolutionTimeInHours = 4 },
                new SlaConfiguration { Id = 2, Priority = Domain.Enums.Priority.Medium, ResolutionTimeInHours = 10 },
                new SlaConfiguration { Id = 3, Priority = Domain.Enums.Priority.Low, ResolutionTimeInHours = 24 }
            );

            modelBuilder.Entity<BusinessClosure>().HasData(
                new BusinessClosure
                {
                    Id = 1,
                    Name = "Saudi Founding Day 2026",
                    ClosureDate = new DateTime(2026, 02, 22),
                    IsFullDayClosure = true,
                    Description = "Saudi Founding Day"
                },
                new BusinessClosure
                {
                    Id = 2,
                    Name = "Saudi Nationl Day 2026",
                    ClosureDate = new DateTime(2026, 09, 23),
                    IsFullDayClosure = true,
                    Description = "Saudi Nationl Day"
                },               
                new BusinessClosure
                {
                    Id = 3,
                    Name = "Today event from 6 PM to 10 PM",
                    ClosureDate = new DateTime(2025, 11, 25),
                    IsFullDayClosure = false,
                    StartTime = new TimeSpan(18, 0, 0),
                    EndTime = new TimeSpan(22, 0, 0),
                    Description = "Today Event for 4 Hours"
                }
            );
        }
    }
}
