namespace Quick.Common.Configuration.Exceptions
{
    public class ConfigurationSectionNotFoundException : Exception
    {
        public ConfigurationSectionNotFoundException(string key) : 
            base($"Секция конфигурации не найдена по ключу '{key}'")
        {

        }
    }
}
