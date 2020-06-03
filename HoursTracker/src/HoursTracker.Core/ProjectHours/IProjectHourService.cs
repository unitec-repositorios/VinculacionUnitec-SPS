using HoursTracker.Domain.Aggregates.Projecthours;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HoursTracker.Core.Projecthours
{
    public interface IProjecthourService
    {
        /*
        Task<SingleProjectHourDto> FindById(int id);

        Task Create(ProjectHour projecthour);

        Task<IEnumerable<SingleProjectHourDto>> All();

        Task Remove(int id);

        Task Update(int id, ProjectHour projectHour);
        */

        //Task Update(int id, updateProjectHourDto projecthour);


        //Code for Demostration purpose
        Task<Projecthour> FindById(int id);

        Task Create(Projecthour projecthour);

        Task<IEnumerable<Projecthour>> All();

        Task Remove(int id);

        Task Update(int id, Projecthour projecthour);
    }
}
