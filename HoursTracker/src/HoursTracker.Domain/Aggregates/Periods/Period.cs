using HoursTracker.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HoursTracker.Domain.Aggregates.Periods
{
    [Table("periodos")]
    public class Period : BaseEntity, IAggregateRoot
    {
        [Column("codigo_periodo")]
        public string Code { get; set; }

        [Column("anio_periodo")]
        public string Year { get; set; }

        [Column("semestre_periodo")]
        public string Semester { get; set; }

        [Column("trimestre_periodo")]
        public string Trimester { get; set; }
    }
}
