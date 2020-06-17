using System.Collections.Generic;
using System.Threading.Tasks;
using HoursTracker.Domain.Aggregates.Students;

namespace HoursTracker.Core.Students
{
    public interface IStudentService
    {
        Task<Student> FindByCode(int code);
        Task<Student> FindById(int id);

        Task Create(CreateStudentDto student);

        Task<IEnumerable<SingleStudentDto>> All();

        Task Remove(int id);

        Task Update(int id, UpdateSudentDto student);

        
    }
}