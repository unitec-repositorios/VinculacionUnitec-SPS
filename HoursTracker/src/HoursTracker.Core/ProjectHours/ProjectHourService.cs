using HoursTracker.Domain.Aggregates.Projecthours;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HoursTracker.Core.Projecthours
{
    public class ProjecthourService : IProjecthourService
    {
        private readonly IProjecthourRepository _projecthourRepository;

        public ProjecthourService(IProjecthourRepository projecthourRepository)
        {
            _projecthourRepository = projecthourRepository;
        }

        /*
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
        /*               .ToListAsync();
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
        /*               .FirstOrDefaultAsync();
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
        */

        //Code for Demostration purpose

        public async Task<IEnumerable<Projecthour>> All()
        {
            return await _projecthourRepository
                 .Filter(projecthour => !projecthour.Disabled)
                 .ToListAsync();
        }

        public async Task Create(Projecthour projecthour)
        {
            await _projecthourRepository.Add(projecthour);
        }

        public async Task<Projecthour> FindById(int id)
        {
            return await _projecthourRepository.FindById(id);
        }

        public async Task Remove(int id)
        {
            var projecthour = await _projecthourRepository.FindById(id);
            await _projecthourRepository.Disable(projecthour);
        }

        public async Task Update(int id, Projecthour projecthour)
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
