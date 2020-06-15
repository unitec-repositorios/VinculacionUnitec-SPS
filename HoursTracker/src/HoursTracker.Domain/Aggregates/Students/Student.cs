using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using HoursTracker.Domain.Aggregates.Campuses;
using HoursTracker.Domain.Aggregates.Bot;
using HoursTracker.Domain.Contracts;
using HoursTracker.Domain.Shared;
using HoursTracker.Domain.Aggregates.ProjectHours;

namespace HoursTracker.Domain.Aggregates.Students
{
    [Table("alumnos")]
    public class Student : BaseEntity, IAggregateRoot
    {
        [Column("codigo_alumno")]
        public int Account { get; set; }

        [Column("primer_nombre")]
        public string FirstName { get; set; }

        [Column("segundo_nombre")]
        public string SecondName { get; set; }

        [Column("primer_apellido")]
        public string FirstSurname { get; set; }

        [Column("segundo_apellido")]
        public string SecondSurname { get; set; }

        [Column("finiquito")]
        public bool Settlement { get; set; }

        [Column("correo_electronico")]
        public string Email { get; set; }

        [Column("id_campus")]
        public int CampusId { get; set; }
        public Campus Campus { get; set; }

        public DataBot Data { get; set; }

        public ICollection<StudentCareer> StudentCareers { get; set; } = new HashSet<StudentCareer>();

        public ICollection<StudentSection> StudentSections { get; set; } = new HashSet<StudentSection>();

        public ICollection<ProjectHour> ProjectHours { get; set; } = new HashSet<ProjectHour>();
    }
}