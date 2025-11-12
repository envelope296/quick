using Quick.BusinessLogic.Repositories.Abstractions;
using Quick.DataAccess.Context.Contexts;
using Quick.DataAccess.Context.Repositories;
using Quick.DataAccess.Models;

namespace Quick.BusinessLogic.Repositories.Implementations
{
    public class SubgroupRepository : BaseEntityWithIdRepository<ApplicationDbContext, Subgroup, Guid>, ISubgroupRepository
    {
        public SubgroupRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
