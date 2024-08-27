using MessageHub.Models;

namespace MessageHub.AppService.Interfaces
{
    public interface IMessageHubService
    {
        public Task<MessageDto?> AddAsync(MessageDto? message);

        public Task<ListMessageDto> GetMessagesAsync(TimeSpan timeInterval);
    }
}
