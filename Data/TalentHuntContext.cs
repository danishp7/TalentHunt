using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentHunt.Models;

namespace TalentHunt.Data
{
    public class TalentHuntContext : IdentityDbContext<User>
    {
        public TalentHuntContext(DbContextOptions<TalentHuntContext> options) : base(options) { }

        // add tables
        public DbSet<User> AppUsers { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        // public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<Responsibility> Responsibilities { get; set; }
        public DbSet<KeyResponsibility> KeyResponsibilities { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Institute> Institutes { get; set; }
        public DbSet<EducationLevel> EducationLevels { get; set; }
        public DbSet<Employment> Employments { get; set; }
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // for identityuserlogin
            base.OnModelCreating(modelBuilder);

            // relation of vacancy and responsibility

            // set the composite key
            modelBuilder.Entity<KeyResponsibility>()
                .HasKey(k => new { k.VacancyId, k.ResponsibilityId });

            // 1 responsibility can be in many vacancies
            modelBuilder.Entity<KeyResponsibility>()
                .HasOne(r => r.Responsibility)
                .WithMany(r => r.KeyResponsibilities)
                .HasForeignKey(r => r.ResponsibilityId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1 vacancy can have many responsibilities
            modelBuilder.Entity<KeyResponsibility>()
                .HasOne(v => v.Vacancy)
                .WithMany(v => v.KeyResponsibilities)
                .HasForeignKey(v => v.VacancyId)
                .OnDelete(DeleteBehavior.Cascade);

            //----------------------------------------------

            // relation of vacancy and experience

            // 1 responsibility can be in many vacancies
            modelBuilder.Entity<Experience>()
                .HasMany(e => e.Vacancies)
                .WithOne(v => v.Experience)
                .OnDelete(DeleteBehavior.Cascade);

            //----------------------------------------------

            // relation of vacancy and application
            modelBuilder.Entity<Vacancy>()
                .HasMany(a => a.Applications)
                .WithOne(v => v.Vacancy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
