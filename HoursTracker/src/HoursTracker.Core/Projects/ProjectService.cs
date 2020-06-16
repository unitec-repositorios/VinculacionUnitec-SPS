using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HoursTracker.Domain.Aggregates.Projects;
using Microsoft.EntityFrameworkCore;

namespace HoursTracker.Core.Projects
{
     public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<IEnumerable<Project>> All()
        {
            return await _projectRepository
                .Filter(project => !project.Disabled)
                .ToListAsync();
        }

        public async Task Create(Project project)
        {
            await _projectRepository.Add(project);
        }

        public async Task<Project> FindById(int id)
        {
            return await _projectRepository.FindById(id);
        }

        public async Task Remove(int id)
        {
            var project = await _projectRepository.FindById(id);
            await _projectRepository.Disable(project);
        }

       public async Task Update(int id, Project professor)
        {
            var existingProject = await _projectRepository.FindById(id);

            existingProject.Code = professor.Code;
            existingProject.Name = professor.Name;
            existingProject.Budget = professor.Budget;
           

            await _projectRepository.Update(existingProject);
        }
    }
}
