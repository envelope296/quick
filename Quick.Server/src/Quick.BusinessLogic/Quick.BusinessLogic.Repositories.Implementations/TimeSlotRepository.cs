using Quick.BusinessLogic.Repositories.Abstractions;
using Quick.DataAccess.Context.Contexts;
using Quick.DataAccess.Context.Repositories;
using Quick.DataAccess.Models;

namespace Quick.BusinessLogic.Repositories.Implementations
{
    public class TimeSlotRepository : BaseEntityWithIdRepository<ApplicationDbContext, TimeSlot, Guid>, ITimeSlotRepository
    {
        public TimeSlotRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
