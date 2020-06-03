using System.Collections.Generic;
using System.Security.Principal;
using System.ComponentModel.DataAnnotations.Schema;
using HoursTracker.Domain.Aggregates.Campuses;
using HoursTracker.Domain.Aggregates.DataBot;
using HoursTracker.Domain.Contracts;
using HoursTracker.Domain.Shared;

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

        public Campus Campus { get; set; }

        public DataBotS Data { get; set; }

        public ICollection<StudentCareer> StudentCareers { get; set; } = new HashSet<StudentCareer>();
    }
}