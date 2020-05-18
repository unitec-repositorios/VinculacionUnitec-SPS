using HoursTracker.Domain.Aggregates.Students;
using Microsoft.EntityFrameworkCore;

namespace HoursTracker.Data.Repositories.Students
{
    public class StudentRepository : EfRepository<Student>, IStudentRepository
    {
        public StudentRepository(DbContext context) : base(context)
        {

        }
    }
}