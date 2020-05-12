using HoursTracker.Domain.Aggregates.Campuses;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HoursTracker.Core.Campuses
{
    public interface ICampusService
    {
        Task<Domain.Aggregates.Campuses.Campus> FindById(int id);

        Task Create(Domain.Aggregates.Campuses.Campus campus);

        Task<IEnumerable<Domain.Aggregates.Campuses.Campus>> All();

        Task Remove(int id);

        Task Update(int id, Domain.Aggregates.Campuses.Campus campus);
    }
}
