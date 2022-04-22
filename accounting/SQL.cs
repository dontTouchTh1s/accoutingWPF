using System.Data.SqlClient;
using System.Collections.Generic;

namespace accounting
{
    public class SqlConnect
    {
        private const string ConnectionString = "Server=ALIM;Database=accounting;Trusted_Connection=True;";
        private SqlConnection _connection;
        public void Open()
        {
            _connection = new(ConnectionString);
            _connection.Open();
        }
        public void Close()
        {
            _connection.Close();
        }
        public SqlDataReader Query(string query, params string[] values)
        {

            SqlCommand command = new(query, _connection);
            for (int i = 0; i < values.Length; i++)
            {
                command.Parameters.AddWithValue("@" + i.ToString(), values[i]);
            }
            SqlDataReader reader = command.ExecuteReader();
            return reader;
        }

    }
}
