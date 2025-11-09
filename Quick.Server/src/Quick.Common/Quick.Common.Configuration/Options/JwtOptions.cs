namespace Quick.Common.Configuration.Options
{
    public class JwtOptions
    {
        public static string ConfigurationKey = "JWT";

        public string SecretKey { get; set; } = null!;

        public int ExpirationHours { get; set; }
    }
}
