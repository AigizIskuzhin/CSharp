namespace MyBus.Infrastructure.Services.MySql.MySqlExtenstions
{
    public static class MySqlServiceCommonExtenstions
    {
        public static bool IsUserExist(this MySqlService mySqlService, string phone) =>
            mySqlService.MySqlDataReader($"select id from user where phone={phone}").HasRows;

        public static bool IsUserPasswordCorrect(this MySqlService mySqlService, string phone, string password) =>
            mySqlService.MySqlDataReader($"select id from user where phone={phone} and password={password}").HasRows;
    }
}
