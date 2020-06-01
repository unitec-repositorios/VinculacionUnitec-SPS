using HoursTracker.Domain.Aggregates.Classes;
using HoursTracker.Domain.Aggregates.Professors;
using HoursTracker.Domain.Contracts;
using HoursTracker.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Domain.Aggregates.Sections
{
    public class Section : BaseEntity, IAggregateRoot
    {
        public string SectionCode { get; set; }
        /* public Periodo periodo { get; set; } */
        public Class Clase { get; set; }
        public Professor Professor { get; set; }
        public ICollection<SectionStudent> SectionStudents { get; set; } = new HashSet<SectionStudent>();
        public ICollection<SectionProject> SectionProjects { get; set; } = new HashSet<SectionProject>();
    }
}

