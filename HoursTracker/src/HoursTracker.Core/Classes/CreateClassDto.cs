using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Core.Classes
{
    public class CreateClassDto
    {
        public int Id { get; set; }

        public string ClassName { get; set; }

        public string ClassCode { get; set; }

        public IEnumerable<int> Careers { get; set; }
    }
}
