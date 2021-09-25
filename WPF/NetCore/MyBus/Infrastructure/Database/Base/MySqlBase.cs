using System;
using MySql.Data.MySqlClient;

namespace MyBus.Infrastructure.Database.Base
{
    public class MySqlBase
    {
        public string ConnectionString => "server=localhost;" +
                                          "user=root;" +
                                          "password=admin;" +
                                          "database=test;";

        public MySqlConnection MySqlConnection = new("server=localhost;" +
                                                     "user=root;" +
                                                     "password=admin;" +
                                                     "database=test;");

        private readonly MySqlCommand _MySqlCommand;
        public MySqlCommand MySqlCommand(string cmd)
        {
            _MySqlCommand.Dispose();
            _MySqlCommand.CommandText = cmd;
            return _MySqlCommand;
        }

        public MySqlBase()
        {
            _MySqlCommand ??= new MySqlCommand("Select 1", MySqlConnection);
            if (ConnectionIsAvailable())
            {
                MySqlConnection.Open();
                _MySqlCommand.ExecuteNonQuery();
                MySqlConnection.Close();
            }
                
        }

        public bool ConnectionIsAvailable()
        {
            var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
                connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
