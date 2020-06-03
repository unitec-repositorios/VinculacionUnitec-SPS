using HoursTracker.Domain.Aggregates.Projects;
using HoursTracker.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HoursTracker.Domain.Aggregates.VinculationTypes
{
    [Table("tipos_vinculacion")]
    public class VinculationType :  BaseEntity, IAggregateRoot
    {
        [Column("codigo_vinculacion")]
        public string Code { get; set; }

        [Column("tipo_vinculacion")]
        public string Type { get; set; }

        public ICollection<Project> Project { get; set; } = new HashSet<Project>();
    }
}
