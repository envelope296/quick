namespace Quick.BusinessLogic.Contracts.Responses.Common
{
    public class PageResponse<T>
    {
        private static readonly PageResponse<T> _emptyInstance = new PageResponse<T> { Items = new List<T>(), TotalCount = 0 };

        public required List<T> Items { get; set; }

        public required int TotalCount { get; set; }

        public static PageResponse<T> Empty => _emptyInstance;
    }
}
