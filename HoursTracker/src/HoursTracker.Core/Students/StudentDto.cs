using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Core.Students
{
    public class StudentDto
    {
        public int Id { get; set; }

        public int Account { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string FirstSurname { get; set; }

        public string SecondSurname { get; set; }

        public int Settlement { get; set; }

        public string CampusName { get; set; }

        public string CareerName { get; set; }

    }
}
