using System.Collections.Generic;
using System.Threading.Tasks;
using HoursTracker.Domain.Aggregates.Professors;
using Microsoft.EntityFrameworkCore;

namespace HoursTracker.Core.Professors
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;

        public ProfessorService(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public async Task<Professor> FindById(int id)
        {
            return await _professorRepository.FindById(id);
        }

        public async Task Create(Professor professor)
        {
            await _professorRepository.Add(professor);
        }

        public async Task<IEnumerable<Professor>> All()
        {
            return await _professorRepository
                .Filter(professor => !professor.Disabled)
                .ToListAsync();
        }

        public async Task Remove(int id)
        {
            var professor = await _professorRepository.FindById(id);
            await _professorRepository.Disable(professor);
        }

        public async Task Update(int id, Professor professor)
        {
            var existingProfessor = await _professorRepository.FindById(id);

            existingProfessor.Code = professor.Code;
            existingProfessor.FirstName = professor.FirstName;
            existingProfessor.SecondName = professor.SecondName;
            existingProfessor.FirstLastName = professor.FirstLastName;
            existingProfessor.SecondLastName = professor.SecondLastName;

            await _professorRepository.Update(existingProfessor);
        }
    }
}