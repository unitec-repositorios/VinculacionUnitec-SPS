using HoursTracker.Domain.Aggregates.Classes;
using HoursTracker.Domain.Aggregates.Faculties;
using HoursTracker.Domain.Contracts;
using HoursTracker.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HoursTracker.Domain.Aggregates.Careers
{
    [Table("carreras")]
    public class Career : BaseEntity, IAggregateRoot
    {
        [Column("nombre_carrera")]
        public string Name { get; set; }

        [Column("codigo_carrera")]
        public string Code { get; set; }

        public ICollection<ClassCareer> ClassCareers { get; set; } = new HashSet<ClassCareer>();

        public ICollection<StudentCareer> StudentCareers { get; set; } = new HashSet<StudentCareer>();

        public ICollection<CampusCareer> CampusCareer { get; set; } = new HashSet<CampusCareer>();

        [Column("id_facultad")]
        public Faculty Faculty { get; set; }
    }
}
