using HoursTracker.Domain.Aggregates.Campuses;
using HoursTracker.Domain.Aggregates.Professors;
using HoursTracker.Domain.Aggregates.Students;
using Microsoft.EntityFrameworkCore;

namespace HoursTracker.Data.Contexts
{
    public class HoursTrackerContext : DbContext
    {
        public HoursTrackerContext(DbContextOptions options) : base(options)
        {
        }
        
        public  DbSet<Professor> Professors { get; set; }

        public DbSet<Campus> Campuses { get; set; }
        
        public DbSet<Student> Students { get; set; } 
    }
}