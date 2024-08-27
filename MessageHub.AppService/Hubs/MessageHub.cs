using MessageHub.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace MessageHub.AppService.Hubs
{
    public class MessageHub(ILogger<MessageHub> logger) : Hub
    {
        private readonly ILogger<MessageHub> _logger = logger;

        public async Task SendMessage(MessageDto? messageDto)
        {
            try
            {
                if (Clients is null)
                {
                    _logger.LogWarning("{0}\n{1} is NULL", nameof(MessageHub), nameof(Clients));
                    return;
                }

                if(messageDto is null)
                {
                    _logger.LogWarning("{0}\n{1} is NULL", nameof(MessageHub), nameof(messageDto));
                    return;
                }

                await Clients.All.SendAsync("Receive", messageDto);
            }
            catch (Exception e)
            {
                _logger.LogError("{0} - {1}\n{2}", nameof(MessageHub), nameof(SendMessage), e.ToString());
                throw;
            }
        }
    }
}
