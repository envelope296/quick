namespace Quick.BusinessLogic.Contracts.Requests.Base
{
    public abstract class BasePageRequest
    {
        public int Page { get; set; }

        public int Size { get; set; }
    }
}
