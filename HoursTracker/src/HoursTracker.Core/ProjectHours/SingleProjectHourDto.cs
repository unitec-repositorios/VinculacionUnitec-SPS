using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Core.ProjectHours
{
    public class SingleProjectHourDto
    {
        public int HoursWork { get; set; }
        public string StudentName { get; set; }
        public string SectionNumber { get; set; }
        public string ProjectName { get; set; }
    }
}
