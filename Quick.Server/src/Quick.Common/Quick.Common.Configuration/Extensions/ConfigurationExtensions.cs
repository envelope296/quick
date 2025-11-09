using Microsoft.Extensions.Configuration;
using Quick.Common.Configuration.Exceptions;

namespace Quick.Common.Configuration.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T GetRequired<T>(this IConfiguration configuration, string key)
        {
            var value = configuration.GetSection(key).Get<T>();
            if (value == null)
            {
                throw new ConfigurationSectionNotFoundException(key);
            }
            return value;
        }
    }
}
