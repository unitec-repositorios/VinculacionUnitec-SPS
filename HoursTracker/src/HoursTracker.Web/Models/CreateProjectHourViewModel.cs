using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoursTracker.Web.Models
{
    public class CreateProjectHourViewModel
    {
        public int Student { get; set; }

        public int Section { get; set; }

        public int Project { get; set; }

        public int HoursWork { get; set; }

    }
}
