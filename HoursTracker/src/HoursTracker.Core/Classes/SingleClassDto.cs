using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Core.Classes
{
     public class SingleClassDto
    {
        public int Id { get; set; }

        public string ClassName { get; set; }

        public string ClassCode { get; set; }

        public string CareerName { get; set; }
        public IEnumerable<string> Careers { get; set; }
    }
}
