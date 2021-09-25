using System;
using MyBus.Models.User.Base;

namespace MyBus.Infrastructure.Services.Interfaces
{
    public interface IUserDialog
    {
        public bool Edit(User user);
        public bool Add(Type rank);
    }
}
