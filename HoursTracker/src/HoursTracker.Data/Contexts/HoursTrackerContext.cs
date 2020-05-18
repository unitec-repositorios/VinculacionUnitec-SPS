using HoursTracker.Domain.Aggregates.Campuses;
using HoursTracker.Domain.Aggregates.Careers;
using HoursTracker.Domain.Aggregates.Classes;
using HoursTracker.Domain.Aggregates.Faculties;
using HoursTracker.Domain.Aggregates.Professors;
using HoursTracker.Domain.Aggregates.Students;
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

        public DbSet<Student> Students { get; set; }

        public DbSet<Faculty> Faculties { get; set; }


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


            modelBuilder.Entity<StudentCareer>()
                .HasKey(studentCareer => new { studentCareer.StudentId, studentCareer.CareerId });

            modelBuilder.Entity<StudentCareer>()
                .HasOne(studentCareer => studentCareer.Student)
                .WithMany(@student => @student.StudentCareers)
                .HasForeignKey(studentCareer => studentCareer.StudentId);

            modelBuilder.Entity<StudentCareer>()
               .HasOne(studentCareer => studentCareer.Career)
               .WithMany(career => career.StudentCareers)
               .HasForeignKey(studentCareer => studentCareer.CareerId);

            modelBuilder.Entity<CampusCareer>()
               .HasKey(campusCareer => new { campusCareer.CampusId, campusCareer.CareerId });

            modelBuilder.Entity<CampusCareer>()
                .HasOne(campusCareer => campusCareer.Campus)
                .WithMany(@campus => @campus.CampusCareer)
                .HasForeignKey(campusCareer => campusCareer.CampusId);

            modelBuilder.Entity<CampusCareer>()
               .HasOne(campusCareer => campusCareer.Career)
               .WithMany(career => career.CampusCareer)
               .HasForeignKey(campusCareer => campusCareer.CareerId);

            modelBuilder.Entity<Faculty>()
                .HasMany(f => f.Career)
                .WithOne(c => c.Faculty);


            modelBuilder.Entity<Student>()
                .HasOne(entity => entity.Campus)
                .WithMany(entity => entity.Students);

        }
    }
}