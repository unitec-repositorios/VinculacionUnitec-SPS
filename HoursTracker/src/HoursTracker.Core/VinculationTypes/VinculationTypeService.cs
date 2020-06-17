using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoursTracker.Domain.Aggregates.VinculationTypes;
using Microsoft.EntityFrameworkCore;

namespace HoursTracker.Core.VinculationTypes
{
    public class VinculationTypeService : IVinculationTypeService
    {
        private readonly IVinculationTypeRepository _vinculationTypeRepository;

        public VinculationTypeService(IVinculationTypeRepository vinculationTypeRepository)
        {
            _vinculationTypeRepository = vinculationTypeRepository;
        }
        public async Task<IEnumerable<VinculationType>> All()
        {
            return await _vinculationTypeRepository
                .Filter(vinculation => !vinculation.Disabled)
                .ToListAsync();
        }

        public async Task<VinculationType> FindById(int id)
        {
            var data = _vinculationTypeRepository.All().First(x => x.Id == id);

            return new VinculationType
            {    Id = data.Id,
                Code = data.Code,
                Type = data.Type,
 
            };
        }
    }
}
