using HoursTracker.Domain.Aggregates.Students;
using HoursTracker.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Domain.Aggregates.Projecthours
{
    public class Projecthour : BaseEntity, IAggregateRoot
    {
        public int Hours { get; set; }
        public int Account { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public int SeccionCode { get; set; }
        public string SeccionName { get; set; }
        public string ProjectName { get; set; }
        public string TableState { get; set; }

        //public Student Student { get; set; }

        //public Student Student { get; set; }
        //public Section Section { get; set; }
        //public Project Project { get; set; }
    }
}
