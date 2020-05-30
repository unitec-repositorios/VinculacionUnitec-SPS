using HoursTracker.Domain.Aggregates.ProjectHours;
using HoursTracker.Domain.Aggregates.Students;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HoursTracker.Core.ProjectHours
{
    public class ProjectHourService : IProjectHourService
    {
        private readonly IProjectHourRepository _projectHourRepository;
        private readonly IStudentRepository _studentRepository;
        //private readonly ISectionRepository _sectionRepository;
        //private readonly IProjectRepository _projectRepository;

        public ProjectHourService
            (IProjectHourRepository projectHourRepository,
             IStudentRepository studentRepository)
            //ISectionRepository sectionRepository,
            //IProjectRepository projectRepository)
        {
            _projectHourRepository = projectHourRepository;
            _studentRepository = studentRepository;
            //_sectionRepository = sectionRepository;
            //_projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectHour>> All()
        {
            throw new NotImplementedException();
        }

        public async Task Create(CreateProjectHourDto projecthour)
        {
            var student = await _studentRepository.FindById(projecthour.Student);
            //var section = await _sectionRepository.FindById(projecthour.Section);
            //var project = await _projectRepository.FindById(projecthour.Project);

            var projecthourInfo = new ProjectHour
            {
                HoursWork = projecthour.HoursWork,
                Student = student
                //Section = section,
                //Project = project
            };

            await _projectHourRepository.Add(projecthourInfo);

        }

        public async Task<ProjectHour> FindById(int id)
        {
            return await _projectHourRepository.FindById(id);
        }

        public async Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
