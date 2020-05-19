using HoursTracker.Domain.Aggregates.Faculties;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HoursTracker.Core.Faculties
{
    interface IFacultyService
    {
        Task<Faculty> FindById(int id);

        Task Create(Faculty faculty);

        Task<IEnumerable<Faculty>> All();

        Task Remove(int id);

        Task Update(int id, Faculty faculty);
    }
}
