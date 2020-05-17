using HoursTracker.Domain.Aggregates.Classes;
using HoursTracker.Domain.Aggregates.Faculties;
using HoursTracker.Domain.Contracts;
using HoursTracker.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Domain.Aggregates.Careers
{
    public class Career : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public ICollection<ClassCareer> ClassCareers { get; set; } = new HashSet<ClassCareer>();

        public ICollection<StudentCareer> StudentCareers { get; set; } = new HashSet<StudentCareer>();

        public ICollection<CampusCareer> CampusCareer { get; set; } = new HashSet<CampusCareer>();

        public Faculty Faculty { get; set; }
    }
}
