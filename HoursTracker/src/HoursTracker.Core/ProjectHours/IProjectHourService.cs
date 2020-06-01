using HoursTracker.Domain.Aggregates.ProjectHours;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HoursTracker.Core.ProjectHours
{
    public interface IProjectHourService
    {
        Task<SingleProjectHourDto> FindById(int id);

        Task Create(ProjectHour projecthour);

        Task<IEnumerable<SingleProjectHourDto>> All();

        Task Remove(int id);

        Task Update(int id, ProjectHour projectHour);

        //Task Update(int id, updateProjectHourDto projecthour);
    }
}
