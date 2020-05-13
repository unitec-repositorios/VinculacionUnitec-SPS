using System.Collections.Generic;
using System.Threading.Tasks;
using HoursTracker.Domain.Aggregates.Students;

namespace HoursTracker.Core.Students
{
    public interface IStudentService
    {
        Task<Student> FindById(int id);

        Task Create(Student student);

        Task<IEnumerable<Student>> All();

        Task Remove(int id);

        Task Update(int id, Student student);
    }
}