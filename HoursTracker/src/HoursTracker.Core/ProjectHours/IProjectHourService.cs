using HoursTracker.Domain.Aggregates.ProjectHours;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HoursTracker.Core.ProjectHours
{
    public interface IProjectHourService
    {
        Task<ProjectHour> FindById(int id);

        Task Create(CreateProjectHourDto projecthour);

        Task<IEnumerable<ProjectHour>> All();

        Task Remove(int id);

        //Task Update(int id, updateProjectHourDto projecthour);
    }
}
