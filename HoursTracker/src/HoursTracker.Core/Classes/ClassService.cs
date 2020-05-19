using HoursTracker.Domain.Aggregates.Careers;
using HoursTracker.Domain.Aggregates.Classes;
using HoursTracker.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoursTracker.Core.Classes
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly ICareerRepository _careerRepository;

        public ClassService(IClassRepository classRepository, ICareerRepository careerRepository)
        {
            _classRepository = classRepository;
            _careerRepository = careerRepository;
        }

        public async Task<IEnumerable<Class>> All()
        {
            return await _classRepository
                .Filter(@class => !@class.Disabled)
                .ToListAsync();
        }

        public async Task Create(CreateClassDto @class)
        {
            var careers = _careerRepository.Filter(career => @class.Careers.Contains(career.Id));

            var classInfo = new Class
            {
                ClassCode = @class.ClassCode,
                ClassName = @class.ClassName
            };

            foreach (var career in careers)
            {
                classInfo.ClassCareers.Add(new ClassCareer { Career = career });
            }

            await _classRepository.Add(classInfo);
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

        public async Task Update(int id, UpdateClassDto @class)
        {
            var subject = _classRepository
                .All()
                .Include(x => x.ClassCareers)
                .FirstOrDefault(x => x.Id == id);

            await _classRepository.Update(subject.ClassCareers, @class.Careers
                .Select(x => new ClassCareer
                {
                    CareerId = x,
                    ClassId = id
                }), x => x.CareerId);
        }
    }
}
