using MyBus.Infrastructure.Navigation;
using MyBus.Infrastructure.Navigation.Base;
using MyBus.Models.User;
using MyBus.ViewModels.Base;

namespace MyBus.ViewModels
{
    public class ClientAccountViewModel : ViewModel, INavigationAware
    {
        private protected NavigationManager NavigationManager;
        public void OnNavigatingTo(NavigationManager navigationManager, params object[] args)
        {
            NavigationManager = navigationManager;
        }
        #region Fields

        #region Client
        private Client _Client;
        public Client Client
        {
            get=> _Client;
            set=> Set(ref _Client, value);
        }
        #endregion

        #endregion
    }
}
