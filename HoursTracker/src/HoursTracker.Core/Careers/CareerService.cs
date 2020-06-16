using HoursTracker.Domain.Aggregates.Careers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HoursTracker.Core.Careers
{
    public class CareerService : ICareerService
    {
        private readonly ICareerRepository _careerRepository;

        public CareerService(ICareerRepository careerRepository)
        {
            _careerRepository = careerRepository;
        }

        public async Task<IEnumerable<Career>> All()
        {
            return await _careerRepository
                .Filter(career => !career.Disabled)
                .ToListAsync();
        }
        public async Task<Career> FindByCode(string code)
        {
            return await _careerRepository.FirstOrDefault(c => c.Code == code);
        }
        public async Task Create(Career career)
        {
            await _careerRepository.Add(career);
        }

        public async Task<Career> FindById(int id)
        {
            return await _careerRepository.FindById(id);
        }

        public async Task Remove(int id)
        {
            var career = await _careerRepository.FindById(id);
            await _careerRepository.Disable(career);
        }

        public async Task Update(int id, Career career)
        {
            var existingCareer = await _careerRepository.FindById(id);

            existingCareer.Code = career.Code;
            existingCareer.Name = career.Name;

            await _careerRepository.Update(existingCareer);
        }
    }
}
