using MessageHub.DAL.Entities;

namespace MessageHub.DAL
{
    public interface IMessageHubDbContext
    {
        public Task CreateTable();

        public Task Add(MessageEntity entity);

        public Task<IEnumerable<MessageEntity>> Get(TimeSpan timeSpan);
    }
}
