using HoursTracker.Domain.Aggregates.Sections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HoursTracker.Core.Sections
{
    public interface ISectionService
    {
        Task<Section> FindById(int id);

        Task Create(CreateSectionDto section);

        Task<IEnumerable<SingleSectionDto>> All();

        Task Remove(int id);

        Task Update(int id, UpdateSectionDto section);
    }
}
