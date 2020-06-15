using HoursTracker.Domain.Aggregates.Careers;
using HoursTracker.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HoursTracker.Domain.Aggregates.Faculties
{
    [Table("facultades")]
    public class Faculty : BaseEntity, IAggregateRoot
    {
        [Column("codigo_facultad")]
        public string Code { get; set; }

        [Column("nombre_facultad")]
        public string Name { get; set; }

        public ICollection<Career> Career { get; set; } = new HashSet<Career>();
    }
}
