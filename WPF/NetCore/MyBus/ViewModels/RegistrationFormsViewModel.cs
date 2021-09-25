using System.Windows.Input;
using MyBus.Constants;
using MyBus.Infrastructure.Commands;
using MyBus.Infrastructure.Navigation;
using MyBus.Infrastructure.Navigation.Base;
using MyBus.ViewModels.Base;

namespace MyBus.ViewModels
{
    public class RegistrationFormsViewModel : ViewModel, INavigationAware
    {
        private NavigationManager _NavigationManager;
        public void OnNavigatingTo(NavigationManager navigationManager, params object[] args) => _NavigationManager = navigationManager;


        #region OpenWelcomeViewCommand

        private ICommand _OpenWelcomeViewCommand;

        public ICommand OpenWelcomeViewCommand => _OpenWelcomeViewCommand ??=
            new LambdaCommand(_ => _NavigationManager.Navigate(NavigationKeys.WelcomeView));

        #endregion
    }
}
