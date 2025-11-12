using Quick.BusinessLogic.Repositories.Abstractions;
using Quick.DataAccess.Context.Contexts;
using Quick.DataAccess.Context.Repositories;
using Quick.DataAccess.Models;

namespace Quick.BusinessLogic.Repositories.Implementations
{
    public class SubjectRepository : BaseEntityWithIdRepository<ApplicationDbContext, Subject, Guid>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
