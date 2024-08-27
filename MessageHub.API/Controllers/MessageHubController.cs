namespace MessageHub.API.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class MessageHubController(IMessageHubService messageService, MHub messageHub,
        ILogger<MessageHubController> logger,
        IMessageDtoValidator validator) : Controller
    {
        private readonly IMessageHubService _messageService = messageService;
        private readonly MHub _messageHub = messageHub;
        private readonly ILogger<MessageHubController> _logger = logger;
        private readonly IMessageDtoValidator _validator = validator;

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] MessageDto messageDto)
        {
            try
            {
                var validationResult = await _validator.GetValidationResult(messageDto);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning(validationResult.ToString("\n"));
                    return BadRequest(validationResult.ToString("\n"));
                }

                var messageDtoWithDate = await _messageService.AddAsync(messageDto);
                await _messageHub.SendMessage(messageDtoWithDate);
                return Ok(messageDto);
            }
            catch(Exception e)
            {
                _logger.LogError("{0} - {1}\n{2}", nameof(MessageHubController), nameof(SendMessage),  e.ToString());
                return BadRequest(e.ToString());
            }
        }

        [HttpGet("GetMessagesByInterval")]
        public async Task<IActionResult> GetMessagesByInterval([FromQuery] string? interval)
        {
            try
            {
                if (!TimeSpan.TryParse(interval, out TimeSpan parsedInterval))
                {
                    _logger.LogWarning("Не правильный формат временного интервала - {0}", interval);
                    return BadRequest("Не правильный формат временного интервала");
                }

                var messages = await _messageService.GetMessagesAsync(parsedInterval);
                return Ok(messages);
            }
            catch(Exception e)
            {
                _logger.LogError("{0} - {1}\n{2}", nameof(MessageHubController), nameof(GetMessagesByInterval), e.ToString());
                return BadRequest(e.ToString());
            }     
        }
    }
}
