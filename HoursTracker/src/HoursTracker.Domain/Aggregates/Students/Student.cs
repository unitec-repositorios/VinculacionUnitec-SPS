using System.Security.Principal;
using HoursTracker.Domain.Aggregates.Campuses;
using HoursTracker.Domain.Contracts;

namespace HoursTracker.Domain.Aggregates.Students
{
    public class Student : BaseEntity, IAggregateRoot
    {
        public int Account { get; set; }
        public string FirstName { get; set; } 
        public string SecondName { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        public string MajorCode { get; set; }
        public int Settlement { get; set; }

        public Campus Campus { get; set; }
    }
}