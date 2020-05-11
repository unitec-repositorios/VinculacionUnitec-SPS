namespace HoursTracker.Web.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public int Account { get; set; }
        public string FirstName { get; set; } 
        public string SecondName { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        public string MajorCode { get; set; }
        public string CampusCode { get; set; }
        public int Settlement { get; set; }
    }
}