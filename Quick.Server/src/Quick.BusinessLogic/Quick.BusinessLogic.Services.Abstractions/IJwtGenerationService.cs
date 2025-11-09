namespace Quick.BusinessLogic.Services.Abstractions
{
    public interface IJwtGenerationService
    {
        string GenerateJwtToken(long userId);
    }
}
