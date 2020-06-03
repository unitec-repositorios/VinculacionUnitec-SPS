using HoursTracker.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Domain.Aggregates.ProjectHours
{
    public class ProjectHour : BaseEntity, IAggregateRoot
    {
        public int Account { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public int SeccionCode { get; set; }
        public string SeccionName { get; set; }
        public string ProjectName { get; set; }
        public int Hours { get; set; }
        public string TableState { get; set; }
        //public Student Student { get; set; }
    }
}
