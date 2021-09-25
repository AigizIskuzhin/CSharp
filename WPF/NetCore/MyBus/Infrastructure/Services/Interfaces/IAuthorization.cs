using System;

namespace MyBus.Infrastructure.Services.Interfaces
{
    interface IAuthorization
    {
        public bool IsUserExist(string phone);
        public void LogIn(string phone, string password);
        public bool ValidateUser(string phone, string password);

        public event EventHandler<EventArgs<bool>> IsAuthorized;
        public event EventHandler<EventArgs<int>> Complete;
    }
}
