using HoursTracker.Domain.Aggregates.Campuses;
using HoursTracker.Domain.Aggregates.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HoursTracker.Core.Classes
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<IEnumerable<Class>> All()
        {
            return await _classRepository
                .Filter(@class => !@class.Disabled)
                .ToListAsync();
        }

        public async Task Create(Class @class)
        {
            await _classRepository.Add(@class);
        }

        public async Task<Class> FindById(int id)
        {
            return await _classRepository.FindById(id);
        }

        public async Task Remove(int id)
        {
            var @class = await _classRepository.FindById(id);
            await _classRepository.Disable(@class);
        }

        public async Task Update(int id, Class @class)
        {
            var existingClass = await _classRepository.FindById(id);

            existingClass.ClassName = @class.ClassName;
            existingClass.ClassCode = @class.ClassCode;

            await _classRepository.Update(existingClass);
        }
    }
}
