namespace MessageHub.DAL.Entities
{
    public class MessageEntity
    {
        private MessageEntity()
        { }

        private MessageEntity(string content, long number)
        {
            Id = Guid.NewGuid();
            Content = content;
            CreationDate = DateTime.Now;
            Number = number;
        }

        private MessageEntity(Guid id, string content, DateTime creationDate, long number)
        {
            Id = id;
            Content = content;
            CreationDate = creationDate;
            Number = number;
        }

        public Guid Id { get; }

        public DateTime CreationDate { get; }

        public long Number { get; }

        public string Content { get; } = string.Empty;

        public static MessageEntity Create(string content, long number)
        {
            return new MessageEntity(content, number);
        }

        public static MessageEntity Create(Guid id, string content, DateTime creationDate, long number)
        {
            return new MessageEntity(id, content, creationDate, number);
        }
    }
}
