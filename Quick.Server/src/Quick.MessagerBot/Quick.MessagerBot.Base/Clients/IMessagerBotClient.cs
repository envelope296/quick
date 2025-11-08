using Quick.MessagerBot.Base.Models.Chats;
using Quick.MessagerBot.Base.Models.Requests;
using Quick.MessagerBot.Base.Models.Responses;

namespace Quick.MessagerBot.Base.Clients
{
    public interface IMessagerBotClient
    {
        public Task<GetChatsResponse> GetChatsAsync(GetChatsRequest request, CancellationToken cancellationToken);

        public Task<Chat> GetChatAsync(long chatId, CancellationToken cancellationToken);

        public Task<GetUpdatesResponse> GetUpdatesAsync(GetUpdatesRequest request, CancellationToken cancellationToken);
    }
}
