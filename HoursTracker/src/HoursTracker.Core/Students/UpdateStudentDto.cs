using System.Collections.Generic;

namespace HoursTracker.Core.Students
{
    public class UpdateSudentDto
    {
        public int Id { get; set; }

        public int Account { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string FirstSurname { get; set; }

        public string SecondSurname { get; set; }

        public IEnumerable<int> Careers { get; set; }

        public int Campus { get; set; }

        public bool Settlement { get; set; }

        public string Email { get; set; }
    }
}