using Quick.BusinessLogic.Repositories.Abstractions;
using Quick.DataAccess.Context.Contexts;
using Quick.DataAccess.Context.Repositories;
using Quick.DataAccess.Models;

namespace Quick.BusinessLogic.Repositories.Implementations
{
    public class GroupMemberRepository : BaseEntityRepository<ApplicationDbContext, GroupMember>, IGroupMemberRepository
    {
        public GroupMemberRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
