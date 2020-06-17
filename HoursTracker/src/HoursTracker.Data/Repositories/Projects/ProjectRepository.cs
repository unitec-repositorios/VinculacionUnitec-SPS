using HoursTracker.Domain.Aggregates.Projects;
using Microsoft.EntityFrameworkCore;

namespace HoursTracker.Data.Repositories.Projects
{
    public class ProjectRepository : EfRepository<Project>, IProjectRepository
    {
        public ProjectRepository(DbContext context) : base(context)
        {
        }
    }
}

