using HoursTracker.Domain.Aggregates.ProjectHours;
using HoursTracker.Domain.Aggregates.Students;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HoursTracker.Core.ProjectHours
{
    public class ProjectHourService
    {
        private readonly IProjectHourRepository _projecthourRepository;
        private readonly IStudentRepository _studentRepository;

        public ProjectHourService(IProjectHourRepository projecthourRepository, IStudentRepository studentRepository)
        {
            _projecthourRepository = projecthourRepository;
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<ProjectHour>> All()
        {
            return await _projecthourRepository
                .Filter(projecthour => !projecthour.Disabled)
                /*.Select(projecthour => new SingleProjectHourDto  //Error implicit convertion
                {
                    Id = projecthour.Id,
                    Hours = projecthour.Hours,
                    StudentAccount = projecthour.Student.Account
                })*/ 
                .ToListAsync();
        }

        public async Task Create(CreateProjectHourDto projecthour)
        {
            //var student = await _studentRepository.FindById(projecthour.StudentAccount);

            var projecthourInfo = new ProjectHour
            {
                Hours = projecthour.Hours
            };
            await _projecthourRepository.Add(projecthourInfo);
        }

        public async Task<ProjectHour> FindById(int id)
        {
            return await _projecthourRepository
               .Filter(projecthour => !projecthour.Disabled && projecthour.Id == id)
               /*.Select(projecthour => new SingleProjectHourDto
               {
                   Id = projecthour.Id,
                   Hours = projecthour.Hours,
                   StudentAccount = projecthour.Student.Account
               })*/
               .FirstOrDefaultAsync();
        }

        public async Task Remove(int id)
        {
            var faculty = await _projecthourRepository.FindById(id);
            await _projecthourRepository.Disable(faculty);
        }

        public async Task Update(int id, ProjectHour projecthour)
        {
            var existingprojecthour = await _projecthourRepository.FindById(id);

            existingprojecthour.Hours = projecthour.Hours;

            await _projecthourRepository.Update(existingprojecthour);
        }
    }
}
