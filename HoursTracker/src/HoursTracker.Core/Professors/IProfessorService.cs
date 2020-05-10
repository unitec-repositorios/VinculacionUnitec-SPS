using System.Collections.Generic;
using System.Threading.Tasks;
using HoursTracker.Domain.Aggregates.Professors;

namespace HoursTracker.Core.Professors
{
    public interface IProfessorService
    {
        Task<Professor> FindById(int id);
        
        Task Create(Professor professor);

        Task<IEnumerable<Professor>> All();

        Task Remove(int id);

        Task Update(int id, Professor professor);
    }
}