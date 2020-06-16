using HoursTracker.Domain.Aggregates.Sections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HoursTracker.Core.Sections
{
    public interface ISectionService
    {
        Task<Section> FindById(int id);

        Task Create(Section section);

        Task<IEnumerable<Section>> All();

        Task Remove(int id);

        Task Update(int id, Section section);
    }
}
