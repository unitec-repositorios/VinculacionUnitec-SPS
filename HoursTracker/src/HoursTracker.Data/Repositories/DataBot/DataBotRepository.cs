using HoursTracker.Domain.Aggregates.DataBot;
using Microsoft.EntityFrameworkCore;

namespace HoursTracker.Data.Repositories.DataBot
{
    public class DataBotRepository : EfRepository<DataBotS>, IDataBotRepository
    {
        public DataBotRepository(DbContext context) : base(context)
        {

        }
    }
}
