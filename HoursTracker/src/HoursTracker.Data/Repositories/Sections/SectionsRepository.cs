using HoursTracker.Domain.Aggregates.Sections;
using Microsoft.EntityFrameworkCore;

namespace HoursTracker.Data.Repositories.Sections
{
    public class SectionRepository : EfRepository<Section>, ISectionRepository
    {
        public SectionRepository(DbContext context) : base(context)
        {

        }
    }
}