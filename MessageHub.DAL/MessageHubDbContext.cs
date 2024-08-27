using MessageHub.DAL.Entities;
using Microsoft.Extensions.Logging;
using Npgsql;
using NpgsqlTypes;

namespace MessageHub.DAL
{
    public class MessageHubDbContext(ILogger<MessageHubDbContext> logger, 
        string? conncetionString) : IMessageHubDbContext
    {
        private readonly ILogger<MessageHubDbContext> _logger = logger;
        private readonly string _connectionString = conncetionString 
            ?? throw new ArgumentNullException(conncetionString);
       
        public async Task CreateTable()
        {
            try
            {
                await using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();
                await using var command = new NpgsqlCommand(SqlCommand.CREATE_TABLE, connection);
                await command.ExecuteNonQueryAsync();
                _logger.LogInformation("{0}\n{1}", nameof(MessageHubDbContext), command.CommandText);
                await connection.CloseAsync();
            }
            catch(Exception e)
            {
                _logger.LogError("{0}\n{1}", nameof(MessageHubDbContext), e.ToString());
                throw;
            }
        }

        public async Task Add(MessageEntity entity)
        {
            try
            {
                await using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();
                await using var command = new NpgsqlCommand(SqlCommand.INSERT, connection);
                command.Parameters.AddWithValue(NpgsqlDbType.Uuid, entity.Id);
                command.Parameters.AddWithValue(NpgsqlDbType.Varchar, entity.Content);
                command.Parameters.AddWithValue(NpgsqlDbType.Timestamp, entity.CreationDate);
                command.Parameters.AddWithValue(NpgsqlDbType.Bigint, entity.Number);
                await command.ExecuteNonQueryAsync();
                _logger.LogInformation("{0}\n{1}", nameof(MessageHubDbContext), command.CommandText);
                await connection.CloseAsync();
            }
            catch(Exception e)
            {
                _logger.LogError("{0}\n{1}", nameof(MessageHubDbContext), e.ToString());
                throw;
            }
        }

        public async Task<IEnumerable<MessageEntity>> Get(TimeSpan interval)
        {
            try
            {
                await using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();
                await using var command = new NpgsqlCommand(SqlCommand.READ_TABLE, connection);
                command.Parameters.AddWithValue(NpgsqlDbType.Timestamp, DateTime.Now);
                command.Parameters.AddWithValue(NpgsqlDbType.Interval, interval);
                await using var reader = await command.ExecuteReaderAsync();
                var messages = new List<MessageEntity>();
                while (await reader.ReadAsync())
                {
                    messages.Add(MessageEntity.Create(
                        reader.GetGuid(0),
                        reader.GetString(1),
                        reader.GetDateTime(2),
                        reader.GetInt64(3)
                    ));
                }

                _logger.LogInformation("{0}\n{1}", nameof(MessageHubDbContext), command.CommandText);
                await connection.CloseAsync();
                return messages;
            }
            catch(Exception e)
            {
                _logger.LogError("{0}\n{1}", nameof(MessageHubDbContext), e.ToString());
                throw;
            }
        }
    }
}
