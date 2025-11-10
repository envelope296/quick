namespace Quick.BusinessLogic.Contracts.Responses.Error
{
    public class ErrorResponse
    {
        public required int Code { get; set; }

        public required string Message { get; set; }

        public string? ErrorId { get; set; }
    }
}
