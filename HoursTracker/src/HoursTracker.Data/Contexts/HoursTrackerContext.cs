using HoursTracker.Domain.Aggregates.Campuses;
using HoursTracker.Domain.Aggregates.Careers;
using HoursTracker.Domain.Aggregates.Classes;
using HoursTracker.Domain.Aggregates.Professors;
using HoursTracker.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace HoursTracker.Data.Contexts
{
    public class HoursTrackerContext : DbContext
    {
        public HoursTrackerContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Professor> Professors { get; set; }

        public DbSet<Campus> Campuses { get; set; }
        
        public DbSet<Career> Careers { get; set;  } 
        
        public DbSet<Class> Classes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassCareer>()
                .HasKey(classCareer => new { classCareer.ClassId, classCareer.CareerId });

            modelBuilder.Entity<ClassCareer>()
                .HasOne(classCareer => classCareer.Class)
                .WithMany(@class => @class.ClassCareers)
                .HasForeignKey(classCareer => classCareer.ClassId);

            modelBuilder.Entity<ClassCareer>()
                .HasOne(classCareer => classCareer.Career)
                .WithMany(career => career.ClassCareers)
                .HasForeignKey(classCareer => classCareer.CareerId);
        }
    }
}