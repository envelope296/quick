namespace Quick.BusinessLogic.Contracts.Exceptions.Users
{
    public class InvalidInitDataException : Exception
    {
        public InvalidInitDataException(string message) : base(message)
        { 

        }

        public InvalidInitDataException() : base("Невалидные стартовые параметры пользователя")
        {

        }

        public static InvalidInitDataException MissingRequiredKey(string key)
        {
            return new InvalidInitDataException($"Невалидные стартовые параметры пользователя: отсутсвует обязательный ключ '{key}'");
        }
    }
}
