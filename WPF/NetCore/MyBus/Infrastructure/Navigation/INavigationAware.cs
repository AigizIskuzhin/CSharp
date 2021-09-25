using MyBus.Infrastructure.Navigation.Base;

namespace MyBus.Infrastructure.Navigation
{
    public interface INavigationAware
    {
        public void OnNavigatingTo(NavigationManager navigationManager, params object[] args);  
    }
}
