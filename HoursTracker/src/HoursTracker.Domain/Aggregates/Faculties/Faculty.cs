using HoursTracker.Domain.Aggregates.Careers;
using HoursTracker.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Domain.Aggregates.Faculties
{
    public class Faculty : BaseEntity, IAggregateRoot
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public ICollection<Career> Career { get; set; } = new HashSet<Career>();
    }
}
