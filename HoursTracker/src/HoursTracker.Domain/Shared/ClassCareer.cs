using HoursTracker.Domain.Aggregates.Careers;
using HoursTracker.Domain.Aggregates.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Domain.Shared
{
    public class ClassCareer
    {
        public int ClassId { get; set; }

        public int CareerId { get; set; }

        public Class Class { get; set; }

        public Career Career { get; set; }
    }
}
