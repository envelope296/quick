namespace Quick.Common.Context.Exceptions
{
    public class UserContextIsEmptyException : Exception
    {
        public UserContextIsEmptyException() : base("Контекст пользователя пуст")
        {

        }
    }
}
