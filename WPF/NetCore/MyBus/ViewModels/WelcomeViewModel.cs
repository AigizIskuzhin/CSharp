using System.Globalization;
using System.Windows.Input;
using MyBus.Constants;
using MyBus.Infrastructure.Commands;
using MyBus.Infrastructure.Navigation;
using MyBus.Infrastructure.Navigation.Base;
using MyBus.ViewModels.Base;

namespace MyBus.ViewModels
{
    public class WelcomeViewModel : ViewModel, INavigationAware
    {
        private NavigationManager _NavigationManager;
        public void OnNavigatingTo(NavigationManager navigationManager, params object[] args) => _NavigationManager = navigationManager;
        
        #region SelectedLanguage
        private CultureInfo _SelectedLanguage = App.Language;
        public CultureInfo SelectedLanguage
        {
            get=> _SelectedLanguage;
            set
            {
                if (!Set(ref _SelectedLanguage, value)) return;
                App.Language = value;
            }
        }

        #endregion

        #region OpenLoginViewCommand

        private ICommand _OpenLoginViewCommand;

        public ICommand OpenLoginViewCommand => _OpenLoginViewCommand ??=
            new LambdaCommand(_ => _NavigationManager.Navigate(NavigationKeys.LoginFormView));
        

        #endregion

        #region OpenRegistrationFormsViewCommand

        private ICommand _OpenRegistrationFormsViewCommand;
        public ICommand OpenRegistrationFormsViewCommand => _OpenRegistrationFormsViewCommand ??=
            new LambdaCommand(_ => _NavigationManager.Navigate(NavigationKeys.RegistrationFormsView));
        
        #endregion
    }
}
