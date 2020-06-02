using System.Collections.Generic;
using HoursTracker.Domain.Aggregates.Campuses;
using HoursTracker.Domain.Aggregates.DataBot;
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

        public bool Settlement { get; set; }

        public string Email { get; set; }

        public Campus Campus { get; set; }

        public DataBotS Data { get; set; }

        public ICollection<StudentCareer> StudentCareers { get; set; } = new HashSet<StudentCareer>();
    }
}