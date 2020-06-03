using System.Collections.Generic;
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

        public async Task<IEnumerable<Student>> All()
        {
            return await _studentRepository.
                Filter(student => !student.Disabled)
                .ToListAsync();
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
           // existingStudent.CampusCode = student.CampusCode;
           // existingStudent.MajorCode = student.MajorCode;
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