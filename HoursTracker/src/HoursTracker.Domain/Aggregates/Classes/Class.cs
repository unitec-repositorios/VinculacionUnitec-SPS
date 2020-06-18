using HoursTracker.Domain.Aggregates.Careers;
using HoursTracker.Domain.Contracts;
using HoursTracker.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HoursTracker.Domain.Aggregates.Classes
{
    [Table("asignaturas")]
    public class Class : BaseEntity, IAggregateRoot
    {

        [Column("nombre_asignatura")]
        public string ClassName { get; set; }

        [Column("codigo_asignatura")]
        public string ClassCode { get; set; }

        

        
        public ICollection<ClassCareer> ClassCareers { get; set; } = new HashSet<ClassCareer>();
    }
}
