using Quick.BusinessLogic.Repositories.Abstractions;
using Quick.DataAccess.Context.Contexts;
using Quick.DataAccess.Context.Repositories;
using Quick.DataAccess.Models;

namespace Quick.BusinessLogic.Repositories.Implementations
{
    public class TeacherRepository : BaseEntityWithIdRepository<ApplicationDbContext, Teacher, Guid>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
