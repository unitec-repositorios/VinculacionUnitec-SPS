using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoursTracker.Web.Models
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Budget { get; set; }
        public int VinculationTypeId { get; set; }
    }
}
