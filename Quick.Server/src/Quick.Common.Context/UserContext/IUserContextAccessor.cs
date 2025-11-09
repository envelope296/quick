namespace Quick.Common.Context.UserContext
{
    public interface IUserContextAccessor
    {
        UserContext? GetCurrent();

        UserContext GetCurrentOrFail();
    }
}
