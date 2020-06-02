using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoursTracker.Domain.Aggregates.Campuses;
using HoursTracker.Domain.Aggregates.Careers;
using HoursTracker.Domain.Aggregates.DataBot;
using HoursTracker.Domain.Aggregates.Students;
using HoursTracker.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace HoursTracker.Core.Students
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICareerRepository _careerRepository;
        private readonly ICampusRepository _campusRepository;
        private readonly IDataBotRepository _dataBotRepository;

        public StudentService(
            IStudentRepository studentRepository,
            ICareerRepository careerRepository,
            ICampusRepository campusRepository)
        {
            _studentRepository = studentRepository;
            _careerRepository = careerRepository;
            _campusRepository = campusRepository;
        }

        public async Task<Student> FindById(int id)
        {
            return await _studentRepository.FindById(id);
        }

        public async Task<IEnumerable<SingleStudentDto>> All()
        {
            var data = await _studentRepository.
                Filter(student => !student.Disabled)
                .Include(students => students.StudentCareers)
                .ThenInclude(c => c.Career)
                .SelectMany(student => student.StudentCareers,
                    (student, career) => new SingleStudentDto
                    {
                        Id = student.Id,
                        Account = student.Account,
                        CampusName = student.Campus.Name,
                        CareerName = career.Career.Name,
                        FirstName = student.FirstName,
                        FirstSurname = student.FirstSurname,
                        SecondName = student.SecondName,
                        SecondSurname = student.SecondSurname,
                        Settlement = student.Settlement,
                        isInBot = student.Data.Verified == 1
                    })
                    .ToListAsync();

            return data.GroupBy(
               student => student.Id,
               (id, student) => new SingleStudentDto
               {
                   Id = student.First().Id,
                   Account = student.First().Account,
                   CampusName = student.First().CampusName,
                   CareerName = string.Join(", ", student.Select(s => s.CareerName)),
                   FirstName = student.First().FirstName,
                   FirstSurname = student.First().FirstSurname,
                   SecondName = student.First().SecondName,
                   SecondSurname = student.First().SecondSurname,
                   Settlement = student.First().Settlement,
                   isInBot = student.First().isInBot
               }
            );
        }

        public async Task Remove(int id)
        {
            var student = await _studentRepository.FindById(id);
            await _studentRepository.Disable(student);
        }

        public async Task Update(int id, UpdateSudentDto student)
        {
            var stud = _studentRepository
                .All()
                .Include(x => x.StudentCareers)
                .FirstOrDefault(x => x.Id == id);

            stud.Account = student.Account;
            stud.FirstName = student.FirstName;
            stud.SecondName = student.SecondName;
            stud.FirstSurname = student.FirstSurname;
            stud.SecondSurname = student.SecondSurname;
            stud.Settlement = student.Settlement;

            await _studentRepository.Update(stud.StudentCareers, @student.Careers
                .Select(x => new StudentCareer()
                {
                    CareerId = x,
                    StudentId = id
                }), x => x.CareerId);
        }

        public async Task Create(CreateStudentDto student)
        {
            var careers = _careerRepository.Filter(career => student.Careers.Contains(career.Id));
            var campus = await _campusRepository.FindById(student.Campus);

            var studentInfo = new Student
            {
                Account = student.Account,
                FirstName = student.FirstName,
                SecondName = student.SecondName,
                FirstSurname = student.FirstSurname,
                SecondSurname = student.SecondSurname,
                Campus = campus,
                Settlement = student.Settlement,
            };

            foreach (var career in careers)
            {
                studentInfo.StudentCareers.Add(new StudentCareer { Career = career });
            }

            await _studentRepository.Add(studentInfo);
        }
    }
}