using HoursTracker.Domain.Aggregates.Classes;
using HoursTracker.Domain.Aggregates.Periods;
using HoursTracker.Domain.Aggregates.Professors;
using HoursTracker.Domain.Aggregates.Sections;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoursTracker.Core.Sections
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IPeriodRepository _periodRepository;
        private readonly IClassRepository _classRepository;

        public SectionService(
            ISectionRepository sectionRepository,
            IProfessorRepository professorRepository,
            IPeriodRepository periodRepository,
            IClassRepository classRepository) 
        {
            _sectionRepository = sectionRepository;
            _professorRepository = professorRepository;
            _periodRepository = periodRepository;
            _classRepository = classRepository;
        }

        public async Task<Section> FindById(int id)
        {
            return await _sectionRepository.FindById(id);
        }

        public async Task<IEnumerable<SingleSectionDto>> All()
        {
            return await _sectionRepository
                .Filter(section => !section.Disabled)
                .Select(s => new SingleSectionDto
                {
                    Id = s.Id,
                    Class = s.Class.ClassName,
                    Period = s.Period.Code,
                    Professor = s.Professor.FirstName + " " + s.Professor.SecondName + " " + s.Professor.FirstLastName + " " + s.Professor.SecondLastName,
                    Code = s.Code
                }).ToListAsync();
        }

        public async Task Remove(int id)
        {
            var section = await _sectionRepository.FindById(id);
            await _sectionRepository.Disable(section);
        }

        public async Task Update(int id, UpdateSectionDto section)
        {
            var sec = await _sectionRepository.FindById(id);
            var prof = await _professorRepository.FindById(section.Professor);
            var per = await _periodRepository.FindById(section.Period);
            var cla = await _classRepository.FindById(section.Class);

            sec.Professor = prof;
            sec.Period = per;
            sec.Class = cla;
            sec.Code = section.Code;

            await _sectionRepository.Update(sec);
        }

        public async Task Create(CreateSectionDto section)
        {
            var clase = await _classRepository.FindById(section.Class);
            var period = await _periodRepository.FindById(section.Period);
            var professor = await _professorRepository.FindById(section.Professor);

            var sectionInfo = new Section
            {
                Code = section.Code,
                Professor = professor,
                Period = period,
                Class = clase,
            };

            await _sectionRepository.Add(sectionInfo);
        }
    }
}
