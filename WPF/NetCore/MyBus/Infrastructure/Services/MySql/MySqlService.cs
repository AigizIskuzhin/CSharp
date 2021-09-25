using System;
using MyBus.Infrastructure.Services.MySql.Interfaces;
using MySql.Data.MySqlClient;

namespace MyBus.Infrastructure.Services.MySql
{
    public class MySqlService : IMySql
    {
        public static string MySqlConnectionString => "server=localhost;" +
                                                 "user=root;" +
                                                 "password=admin;" +
                                                 "database=mybus;";
        public MySqlConnection MySqlConnection = new(MySqlConnectionString);
        private readonly MySqlCommand _MySqlCommand;
        public MySqlCommand MySqlCommand(string query)
        {
            string q = query;
            _MySqlCommand.CommandText = q;
            return ConnectionIsAvailable ? _MySqlCommand : null;
        }

        public bool Execute(string query)
        {
            string q = query;
            var cmd = MySqlCommand(q);
            cmd?.ExecuteNonQuery();
            return cmd != null;
        }

        public MySqlDataReader MySqlDataReader(string query) => ConnectionIsAvailable ? MySqlCommand(query).ExecuteReader() : null;

        public void Execute(Action query)
        {
            query.Invoke();
        }

        public MySqlService()
        {
            _MySqlCommand = new MySqlCommand("Select 1", MySqlConnection);
            if (ConnectionIsAvailable)
            {
                MySqlConnection.Open();
                _MySqlCommand.ExecuteNonQuery();
                MySqlConnection.Close();
            }
        }

        private bool _ConnectionIsAvailable;    
        public bool ConnectionIsAvailable
        {
            get
            {
                if (_ConnectionIsAvailable) return _ConnectionIsAvailable;
                _ConnectionIsAvailable = CheckConnection();
                return _ConnectionIsAvailable;
            }
        }
        public bool CheckConnection()
        {
            var connection = new MySqlConnection(MySqlConnectionString);
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
