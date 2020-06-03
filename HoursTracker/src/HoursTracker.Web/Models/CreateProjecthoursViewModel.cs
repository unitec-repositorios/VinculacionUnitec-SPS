using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoursTracker.Web.Models
{
    public class CreateProjecthoursViewModel
    {
        public int Hours { get; set; }
        public int Account { get; set; }
        public int SeccionCode { get; set; }
        public string ProjectName { get; set; }
        public string TableState { get; set; }
    }
}
