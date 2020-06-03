using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoursTracker.Web.Models
{
    public class ProjecthourViewModel
    {
        public int Id { get; set; }

        public int Hours { get; set; }

        //public int SectionCode { get; set; }

        //public string ProjectName { get; set; }

        //public int StudentAccount { get; set; }

        //Code for Demostration purpose
        public int Account { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public int SeccionCode { get; set; }
        public string SeccionName { get; set; }
        public string ProjectName { get; set; }
        public string TableState { get; set; }
    }
}
