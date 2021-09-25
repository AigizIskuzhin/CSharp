using System;

namespace MyBus.Infrastructure.Navigation.Interfaces
{
    public interface INavigation
    {
        public void Navigate(Type viewType, params object[] args);
    }
}
