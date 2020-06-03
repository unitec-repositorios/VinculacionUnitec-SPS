using HoursTracker.Domain.Aggregates.ProjectHours;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HoursTracker.Core.ProjectHours
{
    public class ProjectHourService : IProjectHourService
    {
        private readonly IProjectHourRepository _projecthourRepository;
        public ProjectHourService(IProjectHourRepository projecthourRepository)
        {
            _projecthourRepository = projecthourRepository;
        }
        public async Task<IEnumerable<ProjectHour>> All()
        {
            return await _projecthourRepository
                 .Filter(projecthour => !projecthour.Disabled)
                 .ToListAsync();
        }

        public async Task Create(ProjectHour projecthour)
        {
            await _projecthourRepository.Add(projecthour);
        }

        public async Task<ProjectHour> FindById(int id)
        {
            return await _projecthourRepository.FindById(id);
        }

        public async Task Remove(int id)
        {
            var projecthour = await _projecthourRepository.FindById(id);
            await _projecthourRepository.Disable(projecthour);
        }

        public async Task Update(int id, ProjectHour projecthour)
        {
            var existingProjectHour = await _projecthourRepository.FindById(id);

            existingProjectHour.Hours = projecthour.Hours;
            existingProjectHour.Account = projecthour.Account;
            existingProjectHour.StudentFirstName = projecthour.StudentFirstName;
            existingProjectHour.StudentLastName = projecthour.StudentLastName;
            existingProjectHour.SeccionCode = projecthour.SeccionCode;
            existingProjectHour.SeccionName = projecthour.SeccionName;
            existingProjectHour.ProjectName = projecthour.ProjectName;
            existingProjectHour.TableState = projecthour.TableState;

            await _projecthourRepository.Update(existingProjectHour);
        }
    }
}
