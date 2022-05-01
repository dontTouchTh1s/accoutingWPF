using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace accounting
{
    /// <summary>
    ///     Create a sql connection to server and make to run queries
    /// </summary>
    public class SqlConnect
    {
        private const string ConnectionString = "Server=ALIM;Database=accounting;Trusted_Connection=True;";
        private SqlConnection _connection;
        public Task Open()
        {
            _connection = new(ConnectionString);
            return Task.Run(() =>
            {
                _connection.Open();
            });
        }
        public void Close()
        {
            _connection.Close();
        }
        public async Task<SqlDataReader> Query(string query, params string[] values)
        {
            SqlDataReader reader = null;
            await Task.Run(() =>
            {
                SqlCommand command = new(query, _connection);
                for (int i = 0; i < values.Length; i++)
                {
                    command.Parameters.AddWithValue("@" + i.ToString(), values[i]);
                }
                reader = command.ExecuteReader();
            });
            return reader;
        }

    }
}
