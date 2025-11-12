using Quick.BusinessLogic.Repositories.Abstractions;
using Quick.DataAccess.Context.Contexts;
using Quick.DataAccess.Context.Repositories;
using Quick.DataAccess.Models;

namespace Quick.BusinessLogic.Repositories.Implementations
{
    public class LessonTypeRepository : BaseEntityWithIdRepository<ApplicationDbContext, LessonType, Guid>, ILessonTypeRepository
    {
        public LessonTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
