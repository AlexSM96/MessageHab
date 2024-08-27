namespace MessageHub.DAL
{
    internal static class SqlCommand
    {
        public const string CREATE_TABLE = @"
            CREATE TABLE IF NOT EXISTS message (
                id uuid PRIMARY KEY,
                content VARCHAR(128),
                creationdate timestamp without time zone,
                number BIGINT);
        ";

        public const string INSERT = "INSERT INTO message VALUES ($1, $2, $3, $4)";

        public const string READ_TABLE = @"
            SELECT * FROM message 
            WHERE creationdate >= ($1) - ($2)
            ORDER BY creationdate DESC
        ";
    }
}
