using FluentValidation.Results;
using MessageHub.Models;

namespace MessageHub.AppService.Validators
{
    public interface IMessageDtoValidator
    {
        public Task<ValidationResult> GetValidationResult(MessageDto messageDto);
    }
}
