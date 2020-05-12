using HoursTracker.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Domain.Aggregates.Classes
{
    public class Class : BaseEntity, IAggregateRoot
    {
        public string ClassName { get; set; }
    }
}
