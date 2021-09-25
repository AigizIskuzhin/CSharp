using System;
using MySql.Data.MySqlClient;

namespace MyBus.Infrastructure.Services.MySql.Interfaces
{
    public interface IMySql
    {
        public bool Execute(string query);
        public void Execute(Action query);
        public MySqlDataReader MySqlDataReader(string query);
    }
}
