using HoursTracker.Domain.Aggregates.Projects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public async Task Create(Project career)
        {
            throw new NotImplementedException();
        }

        public async Task<Project> FindById(int id)
        {
            return await _projectRepository.FindById(id);
        }

        public async Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int id, Project section)
        {
            throw new NotImplementedException();
        }
    }
}
