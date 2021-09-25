using MySql.Data.MySqlClient;

namespace MyBus.Infrastructure.Database.Base
{
    public static class DBConnection
    {
        public static string ConnectionString => "server=localhost;" +
                                                  "user=root;" +
                                                  "password=admin;" +
                                                  "database=test;";

        public static bool IsConnectionAvailable { get; set; }

        public static void CheckConnection()
        {
            var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.OpenAsync();
                connection.CloseAsync();
                IsConnectionAvailable = true;
            }
            catch
            {
                connection.DisposeAsync();
                IsConnectionAvailable = false;
            }
        }

        private static MySqlConnection _MySqlConnection;
        public static MySqlConnection MySqlConnection => _MySqlConnection ??= new MySqlConnection(ConnectionString);
    }
}
