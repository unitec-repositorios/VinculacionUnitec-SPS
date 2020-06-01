using System.Collections.Generic;
using System.Threading.Tasks;
using HoursTracker.Domain.Aggregates.Sections;
using Microsoft.EntityFrameworkCore;

namespace HoursTracker.Core.Sections
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _sectionRepository;

        public SectionService(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<Section> FindById(int id)
        {
            return await _sectionRepository.FindById(id);
        }

        public async Task Create(Section section)
        {
            await _sectionRepository.Add(section);
        }

        public async Task<IEnumerable<Section>> All()
        {
            return await _sectionRepository
                .Filter(section => !section.Disabled)
                .ToListAsync();
        }

        public async Task Remove(int id)
        {
            var section = await _sectionRepository.FindById(id);
            await _sectionRepository.Disable(section);
        }

        public async Task Update(int id, Section section)
        {
            var existingSection = await _sectionRepository.FindById(id);

            

            await _sectionRepository.Update(existingSection);
        }
    }
}