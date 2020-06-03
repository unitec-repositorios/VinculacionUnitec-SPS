using HoursTracker.Domain.Aggregates.VinculationTypes;
using HoursTracker.Domain.Contracts;
using HoursTracker.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HoursTracker.Domain.Aggregates.Projects
{
    [Table("proyectos")]
    public class Project : BaseEntity, IAggregateRoot
    {
        [Column("codigo_proyecto")]
        public string Code { get; set; }

        [Column("nombre_proyecto")]
        public string Name { get; set; }

        [Column("costo_proyecto")]
        public double Budget { get; set; }

        [Column("id_vinculacion")]
        public int VinculationTypeId { get; set; }
        public VinculationType VinculationType { get; set; }

        public ICollection<SectionProject> SectionProjects { get; set; } = new HashSet<SectionProject>();

        public ICollection<ProjectOrganization> ProjectOrganizations { get; set; } = new HashSet<ProjectOrganization>();
    }
}
