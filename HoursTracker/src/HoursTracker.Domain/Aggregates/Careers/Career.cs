using HoursTracker.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Domain.Aggregates.Careers
{
    public class Career : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public string Code { get; set; }
    }
}
