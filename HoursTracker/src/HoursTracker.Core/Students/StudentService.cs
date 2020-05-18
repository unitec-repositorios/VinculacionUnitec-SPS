using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using HoursTracker.Domain.Aggregates.Students;
using Microsoft.EntityFrameworkCore;

namespace HoursTracker.Core.Students
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            this._studentRepository = studentRepository;
        }

        public async Task<Student> FindById(int id)
        {
            return await _studentRepository.FindById(id);
        }

        public async Task<IEnumerable<StudentDto>> All()
        {
            var data = await _studentRepository.
                Filter(student => !student.Disabled)
                .Include(students => students.StudentCareers)
                .ThenInclude(c => c.Career)
                .SelectMany(student => student.StudentCareers,
                    (student, career) => new StudentDto
                    {
                        Id = student.Id,
                        Account = student.Account,
                        CampusName = student.Campus.Code,
                        CareerName = career.Career.Name,
                        FirstName = student.FirstName,
                        FirstSurname = student.FirstSurname,
                        SecondName = student.SecondName,
                        SecondSurname = student.SecondSurname,
                        Settlement = student.Settlement
                    })
                    .ToListAsync();
                    
                    return data.GroupBy(
                       student => student.Id,
                       (id, student) => new StudentDto
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
                       }
                    );
        }

        public async Task Remove(int id)
        {
            var student = await _studentRepository.FindById(id);
            await _studentRepository.Disable(student);
        }

        public async Task Update(int id, Student student)
        {
            var existingStudent = await _studentRepository.FindById(id);

            existingStudent.Account = student.Account;
            existingStudent.Settlement = student.Settlement;
            //existingStudent.CampusCode = student.CampusCode;
            //existingStudent.MajorCode = student.MajorCode;
            existingStudent.FirstName = student.FirstName;
            existingStudent.SecondName = student.SecondName;
            existingStudent.FirstSurname = student.FirstSurname;
            existingStudent.SecondSurname = student.SecondSurname;
            await _studentRepository.Update(existingStudent);
        }

        public async Task Create(Student student)
        {
            await _studentRepository.Add(student);
        }
    }
}