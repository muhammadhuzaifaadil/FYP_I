using Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.DataContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<University> Universities { get; set; } 
        public DbSet<UniversityDepartments> UniversityDepartment { get; set; }
        public DbSet<UniversityDocuments> UniversityDocument { get; set; }

        public DbSet<UniversityFee> UniversityFee { get; set; }
        public DbSet<UniversityCalendar> UniversityCalendar { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ConsultationQuestionModel> ConsultationQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships and constraints here
            modelBuilder.Entity<UniversityDepartments>()
                .HasOne(ud => ud.University)
                .WithMany(u => u.UniversityDepartments)
                .HasForeignKey(ud => ud.UId);

            modelBuilder.Entity<UniversityDocuments>()
                .HasOne(ud => ud.University)
                .WithMany(u => u.UniversityDocuments)
                .HasForeignKey(ud => ud.UId);

            modelBuilder.Entity<UniversityFee>()
                .HasOne(uf => uf.University)
                .WithMany(u => u.UniversityFees)
                .HasForeignKey(uf => uf.UId);

            modelBuilder.Entity<UniversityCalendar>()
                .HasOne(uc => uc.University)
                .WithMany(u => u.UniversityCalendars)
                .HasForeignKey(uc => uc.UId);
           
        }
    }
}
