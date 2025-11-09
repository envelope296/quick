using Quick.BusinessLogic.Services.Abstractions;

namespace Quick.BusinessLogic.Services.Implementations
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetUtcNow() => DateTime.UtcNow;
    }
}
