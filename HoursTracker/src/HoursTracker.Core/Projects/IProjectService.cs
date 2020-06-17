using System.Collections.Generic;
using System.Threading.Tasks;
using HoursTracker.Domain.Aggregates.Projects;

namespace HoursTracker.Core.Projects
{
    public interface IProjectService
    {
        
        Task<Project> FindById(int id);

        Task Create(CreateProjectDto proyect);

        Task<IEnumerable<Project>> All();

        Task Remove(int id);

        Task Update(int id, Project proyect);
    }
}
