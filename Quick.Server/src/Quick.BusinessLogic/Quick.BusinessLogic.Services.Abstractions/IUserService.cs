using Quick.BusinessLogic.Contracts.Requests.Users;

namespace Quick.BusinessLogic.Services.Abstractions
{
    public interface IUserService
    {
        Task<string> GenerateTokenAsync(GenerateUserTokenRequest request, CancellationToken cancellationToken);
    }
}
