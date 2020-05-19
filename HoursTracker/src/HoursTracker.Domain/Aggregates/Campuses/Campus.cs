



using HoursTracker.Domain.Contracts;
using HoursTracker.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Domain.Aggregates.Campuses
{
    public class Campus : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public ICollection<CampusCareer> CampusCareer { get; set; } = new HashSet<CampusCareer>();
    }
}
