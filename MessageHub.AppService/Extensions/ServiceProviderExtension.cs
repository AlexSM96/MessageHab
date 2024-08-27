using MessageHub.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace MessageHub.AppService.Extensions
{
    public static class ServiceProviderExtension
    {
        public async static void CreateTable(this IServiceProvider serviceProvider)
        {
            await using var scope = serviceProvider.CreateAsyncScope();
            var context = scope.ServiceProvider.GetRequiredService<IMessageHubDbContext>();
            await context.CreateTable();
        }
    }
}
