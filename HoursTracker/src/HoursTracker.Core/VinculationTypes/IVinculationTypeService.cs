using System.Collections.Generic;
using System.Threading.Tasks;
using HoursTracker.Domain.Aggregates.VinculationTypes;

namespace HoursTracker.Core.VinculationTypes
{
    public interface IVinculationTypeService 
    {
        Task<VinculationType> FindById(int id);

        Task<IEnumerable<VinculationType>> All();
    }
}
