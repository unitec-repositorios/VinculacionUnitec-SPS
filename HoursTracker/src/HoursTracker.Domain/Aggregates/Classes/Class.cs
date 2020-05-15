using HoursTracker.Domain.Aggregates.Careers;
using HoursTracker.Domain.Contracts;
using HoursTracker.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Domain.Aggregates.Classes
{
    public class Class : BaseEntity, IAggregateRoot
    {
        public string ClassName { get; set; }

        public string ClassCode { get; set; }

        public ICollection<ClassCareer> ClassCareers { get; set; } = new HashSet<ClassCareer>();
    }
}
