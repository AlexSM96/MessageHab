using MessageHub.AppService.Interfaces;
using MessageHub.AppService.Services;
using MessageHub.AppService.Validators;
using MessageHub.DAL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MessageHub.AppService.Extensions
{
    public static class SeviceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            string? connectionString)
        {
            return services
                .AddScoped<IMessageHubDbContext, MessageHubDbContext>(p => 
                    new MessageHubDbContext(p.GetRequiredService<ILogger<MessageHubDbContext>>(), connectionString))
                .AddScoped<IMessageHubService, MessageHubService>()
                .AddScoped<IMessageDtoValidator, MessageDtoValidator>()
                .AddSingleton<Hubs.MessageHub>();
                ;
        }
    }
}
