using HoursTracker.Domain.Aggregates.Sections;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HoursTracker.Core.Sections
{
    public class SectionService : ISectionService
    {

        private readonly ISectionRepository _sectionRepository;

        public SectionService(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }
        public async Task<IEnumerable<Section>> All()
        {
            return await _sectionRepository
               .Filter(section => !section.Disabled)
               .ToListAsync();
        }

        public async Task Create(Section career)
        {
            throw new NotImplementedException();
        }

        public async Task<Section> FindById(int id)
        {
            return await _sectionRepository.FindById(id);
        }

        public async Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int id, Section career)
        {
            throw new NotImplementedException();
        }
    }
}
