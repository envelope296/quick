using Quick.Common.DataAccess.Abstraction.Repositories;
using Quick.DataAccess.Models;

namespace Quick.BusinessLogic.Repositories.Abstractions
{
    public interface ISubjectRepository : IEntityWithIdRepository<Subject, Guid>
    {

    }
}
