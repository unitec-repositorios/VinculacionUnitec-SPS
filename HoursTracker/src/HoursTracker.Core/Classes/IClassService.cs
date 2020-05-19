using HoursTracker.Domain.Aggregates.Classes;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HoursTracker.Core.Classes
{
    public interface IClassService
    {


        Task<Class> FindById(int id);

        Task Create(CreateClassDto classs);

        Task<IEnumerable<Class>> All();

        Task Remove(int id);

        Task Update(int id, Class classs);
    }
}
