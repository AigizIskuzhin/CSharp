using System;
using MyBus.Infrastructure.Services.MySql;
using MyBus.Infrastructure.Services.MySql.MySqlExtenstions;

namespace MyBus.Infrastructure.Services
{
    public class UserAuthorizationService : Interfaces.IAuthorization
    {
        private readonly MySqlService _MySqlService;

        public UserAuthorizationService(MySqlService mySqlService)
        {
            _MySqlService = mySqlService;
        }
        public event EventHandler<EventArgs<bool>> IsAuthorized;
        public event EventHandler<EventArgs<int>> Complete; 
        public bool IsDatabaseConnected => _MySqlService.ConnectionIsAvailable;
        public bool IsUserExist(string phone) => IsDatabaseConnected && _MySqlService.IsUserExist(phone);
        public bool ValidateUser(string phone, string password) => IsDatabaseConnected && _MySqlService.IsUserPasswordCorrect(phone, password);
        public void LogIn(string phone, string password)
        {
            bool isAuthorized = false;
            int endingCode = -1;
            if (_MySqlService.ConnectionIsAvailable)
            {
                endingCode = 0;
                if (_MySqlService.IsUserExist(phone))
                {
                    endingCode = 1;
                    if (_MySqlService.IsUserPasswordCorrect(phone, password))
                    {
                        isAuthorized = true;
                        endingCode = 2;
                    }
                }
            }
            Complete?.Invoke(this, endingCode);
            IsAuthorized?.Invoke(this, isAuthorized);
        }
    }
}
