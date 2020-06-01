using HoursTracker.Domain.Aggregates.Classes;
using HoursTracker.Domain.Aggregates.Sections;
using HoursTracker.Domain.Aggregates.Students;

namespace HoursTracker.Domain.Shared
{
    public class SectionStudent
    {
        public int SectionId { get; set; }
        public int StudentId { get; set; }
        public Section Section { get; set; }
        public Student Student { get; set; }
    }
}
