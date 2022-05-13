using System.Data.SqlClient;
using System.Threading.Tasks;

namespace accounting
{
    /// <summary>
    ///     Create a sql connection to server and make to run queries
    /// </summary>
    public class MsSql
    {
        private readonly string _connectionString;
        private SqlConnection? _connection;

        public MsSql(string connectionString = "Server=ALIM;Database=accounting;Trusted_Connection=True")
        {
            _connectionString = connectionString;
        }

        public Task Open()
        {
            _connection = new SqlConnection(_connectionString);
            return Task.Run(() => { _connection.Open(); });
        }

        public void Close()
        {
            _connection?.Close();
        }

        public async Task<SqlDataReader> Query(string query, params string[] values)
        {
            SqlDataReader reader = null!;
            await Task.Run(() =>
            {
                SqlCommand command = new(query, _connection);
                for (var i = 0; i < values.Length; i++) command.Parameters.AddWithValue("@" + i, values[i]);
                reader = command.ExecuteReader();
            });
            return reader;
        }
    }
}