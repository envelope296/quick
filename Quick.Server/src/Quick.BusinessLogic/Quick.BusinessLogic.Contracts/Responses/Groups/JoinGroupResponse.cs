namespace Quick.BusinessLogic.Contracts.Responses.Groups
{
    public class JoinGroupResponse
    {
        public required Guid GroupId { get; set; }

        public required bool IsSuccess { get; set; }

        public string? ErrorMessage { get; set; }

        public static JoinGroupResponse Success(Guid groupId)
        {
            return new JoinGroupResponse
            {
                GroupId = groupId,
                IsSuccess = true
            };
        }

        public static JoinGroupResponse Error(Guid groupId, string message)
        {
            return new JoinGroupResponse
            {
                GroupId = groupId,
                IsSuccess = false,
                ErrorMessage = message
            };
        }
    }
}
