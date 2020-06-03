using HoursTracker.Domain.Aggregates.Projecthours;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoursTracker.Data.Repositories.Projecthours
{
    public class ProjecthourRepository : EfRepository<Projecthour>, IProjecthourRepository
    {
        public ProjecthourRepository(DbContext context) : base(context)
        {
        }
    }
}
