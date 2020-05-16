using HoursTracker.Domain.Aggregates.Campuses;
using HoursTracker.Domain.Aggregates.Careers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Domain.Shared
{
    public class CampusCareer
    {
        public int CareerId { get; set; }

        public int CampusId { get; set; }

        public Campus Campus { get; set; }

        public Career Career { get; set; }
    }
}
