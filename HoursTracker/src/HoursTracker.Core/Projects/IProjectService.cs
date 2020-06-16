using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HoursTracker.Domain.Aggregates.Projects;
using Microsoft.EntityFrameworkCore;

namespace HoursTracker.Core.Projects
{
    public interface IProjectService
    {
        
        Task<Project> FindById(int id);

        Task Create(Project proyect);

        Task<IEnumerable<Project>> All();

        Task Remove(int id);

        Task Update(int id, Project proyect);
    }
}
