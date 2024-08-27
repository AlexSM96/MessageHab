using FluentValidation;
using FluentValidation.Results;
using MessageHub.Models;

namespace MessageHub.AppService.Validators
{
    public class MessageDtoValidator : AbstractValidator<MessageDto>, IMessageDtoValidator
    {
        public MessageDtoValidator()
        {
            RuleFor(m => m.Content)
                .MaximumLength(180);
            RuleFor(m => m.Number)
                .NotNull()
                .GreaterThan(0);
        }

        public async Task<ValidationResult> GetValidationResult(MessageDto messageDto)
        {
            var validationResult = await ValidateAsync(messageDto);

            if (validationResult is null)
            {
                throw new ArgumentNullException(nameof(validationResult));
            }
            
            return validationResult;
        }
    }
}
