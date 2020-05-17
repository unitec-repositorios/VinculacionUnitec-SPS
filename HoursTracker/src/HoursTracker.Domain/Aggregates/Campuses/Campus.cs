



using HoursTracker.Domain.Aggregates.Students;
using HoursTracker.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Domain.Aggregates.Campuses
{
    public class Campus : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
