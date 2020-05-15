using HoursTracker.Domain.Aggregates.Careers;
using HoursTracker.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Domain.Aggregates.Classes
{
    public class Class : BaseEntity, IAggregateRoot
    {
        public string ClassName { get; set; }

        public string ClassCode { get; set; }

        public ICollection<Career> Careers { get; set; } = new HashSet<Career>();
    }
}
