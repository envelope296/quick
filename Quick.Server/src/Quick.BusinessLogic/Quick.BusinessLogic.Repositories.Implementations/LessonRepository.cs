using Quick.BusinessLogic.Repositories.Abstractions;
using Quick.DataAccess.Context.Contexts;
using Quick.DataAccess.Context.Repositories;
using Quick.DataAccess.Models;

namespace Quick.BusinessLogic.Repositories.Implementations
{
    public class LessonRepository : BaseEntityWithIdRepository<ApplicationDbContext, Lesson, Guid>, ILessonRepository
    {
        public LessonRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
