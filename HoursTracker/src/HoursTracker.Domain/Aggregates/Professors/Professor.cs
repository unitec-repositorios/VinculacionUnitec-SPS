using HoursTracker.Domain.Contracts;

namespace HoursTracker.Domain.Aggregates.Professors
{
    public class Professor : BaseEntity, IAggregateRoot
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string  FirstLastName { get; set; }

        public string SecondLastName { get; set; }
    }
}