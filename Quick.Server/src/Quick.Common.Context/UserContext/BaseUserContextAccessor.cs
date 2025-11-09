using Quick.Common.Context.Exceptions;

namespace Quick.Common.Context.UserContext
{
    public abstract class BaseUserContextAccessor : IUserContextAccessor
    {
        public abstract UserContext? GetCurrent();

        public virtual UserContext GetCurrentOrFail()
        {
            var user = GetCurrent();
            if (user == null)
            {
                throw new UserContextIsEmptyException();
            }
            return user;
        }
    }
}
