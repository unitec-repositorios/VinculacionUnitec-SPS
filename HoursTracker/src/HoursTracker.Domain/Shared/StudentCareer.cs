using HoursTracker.Domain.Aggregates.Careers;
using HoursTracker.Domain.Aggregates.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Domain.Shared
{
    public class StudentCareer
    {
        public int CareerId { get; set; }

        public int StudentId { get; set; }

        public Student Student  { get; set; }

        public Career Career { get; set; }
    }
}
