using Quick.BusinessLogic.Repositories.Abstractions;
using Quick.DataAccess.Context.Contexts;
using Quick.DataAccess.Context.Repositories;
using Quick.DataAccess.Models;

namespace Quick.BusinessLogic.Repositories.Implementations
{
    public class ScheduleRepository : BaseEntityWithIdRepository<ApplicationDbContext, Schedule, Guid>, IScheduleRepository
    {
        public ScheduleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
