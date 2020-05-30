using HoursTracker.Domain.Aggregates.Students;
using HoursTracker.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace HoursTracker.Domain.Aggregates.ProjectHours
{
    public class ProjectHour : BaseEntity, IAggregateRoot
    {
        public int HoursWork {get; set;}
        public Student Student { get; set; }
        
        //public Section Section { get; set; }
        //public Project Project { get; set; }
    }
}
