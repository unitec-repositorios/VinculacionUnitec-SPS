using System.Collections.Generic;
using System.Security.Principal;
using HoursTracker.Domain.Contracts;
using HoursTracker.Domain.Shared;

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
        public string CampusCode { get; set; }
        public int Settlement { get; set; }

        public ICollection<StudentCareer> StudentCareers { get; set; } = new HashSet<StudentCareer>();
        public ICollection<SectionStudent> SectionStudents { get; set; } = new HashSet<SectionStudent>();
    }
}