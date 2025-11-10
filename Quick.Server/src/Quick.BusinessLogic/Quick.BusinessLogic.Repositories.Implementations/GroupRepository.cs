using Quick.BusinessLogic.Repositories.Abstractions;
using Quick.DataAccess.Context.Contexts;
using Quick.DataAccess.Context.Repositories;
using Quick.DataAccess.Models;

namespace Quick.BusinessLogic.Repositories.Implementations
{
    public class GroupRepository : BaseEntityWithIdRepository<ApplicationDbContext, Group, Guid>, IGroupRepository
    {
        public GroupRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
