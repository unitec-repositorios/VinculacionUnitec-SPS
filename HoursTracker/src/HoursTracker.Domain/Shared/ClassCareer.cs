using HoursTracker.Domain.Aggregates.Careers;
using HoursTracker.Domain.Aggregates.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HoursTracker.Domain.Shared
{
    [Table("asignaturas_x_carreras")]
    public class ClassCareer
    {
        [Column("id_asignatura")]
        public int ClassId { get; set; }

        [Column("id_carrera")]
        public int CareerId { get; set; }

        public Class Class { get; set; }

        public Career Career { get; set; }
    }
}
