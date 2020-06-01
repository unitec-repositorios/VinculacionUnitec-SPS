using HoursTracker.Domain.Aggregates.Professors;
using HoursTracker.Domain.Aggregates.Sections;

namespace HoursTracker.Domain.Shared
{
    public class SectionProject
    {
        public int SectionId { get; set; }
        public int ProjectId { get; set; }
        public Section Section { get; set; }
        /* public Project Project { get; set; } */
    }
}
