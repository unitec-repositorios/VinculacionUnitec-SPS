using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Core.Students
{
    public class StudentsHoursReportDto
    {
        public string Account { get; set; }

        public string StudentName { get; set; }

        public string ProjectName { get; set; }

        public string SectionCode { get; set; }

        public string ClassName { get; set; }

        public int HoursAmount { get; set; }
    }
}
