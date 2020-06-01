using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Core.ProjectHours
{
    public class CreateProjectHourDto
    {
        public int StudentAccount { get; set; }

        public int SectionCode { get; set; }

        public string ProjectName { get; set; }

        public int Hours { get; set; }
    }
}
