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
            //var data = await _sectionRepository.
            //    Filter(section => !section.Disabled)
            //    .Include(sections => sections.StudentCareers)
            //    .ThenInclude(c => c.Career)
            //    .SelectMany(section => section.StudentCareers,
            //        (section, career) => new SingleStudentDto
            //        {
            //            Id = section.Id,
            //            Account = section.Account,
            //            CampusName = section.Campus.Name,
            //            CareerName = career.Career.Name,
            //            FirstName = section.FirstName,
            //            FirstSurname = section.FirstSurname,
            //            SecondName = section.SecondName,
            //            SecondSurname = section.SecondSurname,
            //            Settlement = section.Settlement,
            //            Email = section.Email,
            //            isInBot = section.Data.Verified == 1
            //        })
            //        .ToListAsync();

            //return data.GroupBy(
            //   section => section.Id,
            //   (id, section) => new SingleStudentDto
            //   {
            //       Id = section.First().Id,
            //       Account = section.First().Account,
            //       CampusName = section.First().CampusName,
            //       CareerName = string.Join(", ", section.Select(s => s.CareerName)),
            //       FirstName = section.First().FirstName,
            //       FirstSurname = section.First().FirstSurname,
            //       SecondName = section.First().SecondName,
            //       SecondSurname = section.First().SecondSurname,
            //       Settlement = section.First().Settlement,
            //       Email = section.First().Email,
            //       isInBot = section.First().isInBot
            //   }
            //);
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
            //var stud = _sectionRepository
            //    .All()
            //    .Include(x => x.StudentCareers)
            //    .FirstOrDefault(x => x.Id == id);

            //stud.Account = section.Account;
            //stud.FirstName = section.FirstName;
            //stud.SecondName = section.SecondName;
            //stud.FirstSurname = section.FirstSurname;
            //stud.SecondSurname = section.SecondSurname;
            //stud.Settlement = section.Settlement;
            //stud.Email = section.Email;

            //await _sectionRepository.Update(stud.StudentCareers, @section.Careers
            //    .Select(x => new StudentCareer()
            //    {
            //        CareerId = x,
            //        StudentId = id
            //    }), x => x.CareerId);
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

            //foreach (var career in careers)
            //{
            //    sectionInfo.StudentCareers.Add(new StudentCareer { Career = career });
            //}

            await _sectionRepository.Add(sectionInfo);
        }
    }
}
