using MessageHub.AppService.Interfaces;
using MessageHub.DAL;
using MessageHub.DAL.Entities;
using MessageHub.Models;
using Microsoft.Extensions.Logging;

namespace MessageHub.AppService.Services
{
    public class MessageHubService(IMessageHubDbContext dbContext, 
        ILogger<IMessageHubService> logger) : IMessageHubService
    {
        private readonly IMessageHubDbContext _dbContext = dbContext;
        private readonly ILogger<IMessageHubService> _logger = logger;

        public async Task<MessageDto?> AddAsync(MessageDto? message)
        {
            try
            {
                if (message is null)
                {
                    _logger.LogWarning($"{nameof(MessageHubService)} - AddAsync {0}", message);
                    return null;
                }

                var messageEntity = MessageEntity.Create(message.Content, message.Number);
                await _dbContext.Add(messageEntity);

                return new MessageDto(messageEntity.Content, messageEntity.CreationDate, message.Number);
            }
            catch (Exception e)
            {
                _logger.LogError("{0} - {1}\n{2}", nameof(MessageHubService), nameof(AddAsync),  e.ToString());
                throw;
            }
        }

        public async Task<ListMessageDto> GetMessagesAsync(TimeSpan timeInterval)
        {
            try
            {
                var messageEntitys = await _dbContext.Get(timeInterval);
                var messages = new List<MessageDto>();
                foreach (var messageEntity in messageEntitys)
                {
                    messages.Add(new MessageDto(
                        messageEntity.Content,
                        messageEntity.CreationDate,
                        messageEntity.Number));
                }

                return new ListMessageDto(messages);
            }
            catch (Exception e)
            {
                _logger.LogError("{0} - {1}\n{2}", nameof(MessageHubService), nameof(GetMessagesAsync), e.ToString());
                throw;
            }
        }
    }
}
