using System;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Input;
using MyBus.Constants;
using MyBus.Infrastructure;
using MyBus.Infrastructure.Commands;
using MyBus.Infrastructure.Navigation;
using MyBus.Infrastructure.Navigation.Base;
using MyBus.Infrastructure.Navigation.Interfaces;
using MyBus.Infrastructure.Services;
using MyBus.Infrastructure.Services.Interfaces;
using MyBus.Infrastructure.Services.Validation;
using MyBus.Infrastructure.Services.Validation.Base;
using MyBus.Models.User.Base;
using MyBus.ViewModels.Base;
using MyBus.Views;

namespace MyBus.ViewModels
{
    public class LoginFormViewModel : ValidatableViewModel, INavigationAware
    {
        private NavigationManager _NavigationManager;
        private INavigation NavigationManager;
        private ValidationService ValidationService;
        public void OnNavigatingTo(NavigationManager navigationManager, params object[] args)
        {
            NavigationManager = navigationManager;
            _NavigationManager = navigationManager;
            ValidationService = new();
            Authorization ??= new UserAuthorizationService(App.MySqlService);
            Authorization.IsAuthorized += OnAuthorization;
            Authorization.Complete += (o, e) => CompleteCode = e.Arg;
        }

        private IAuthorization Authorization;

        #region Fields
        
        #region Phone
        private string _Phone;
        public string Phone
        {
            get=> _Phone ??= "";
            set => Set(ref _Phone, value);
        }

        #endregion

        #region Password
        private string _Password;
        public string Password
        {
            get=> _Password ??= "";
            set => ValidateSet(ref _Password, value, ValidationService.IsNullOrWhiteSpace(value));
        }

        #endregion
        
        #endregion

        #region Commands

        #region LogInCommand

        private ICommand _LogInCommand;
        public ICommand LogInCommand => _LogInCommand ??= new LambdaCommand(OnLogInCommand, CanLogInCommand);

        private bool CanLogInCommand(object p) => CanLogIn();

        private void OnLogInCommand(object p) => Authorization.LogIn(Phone, Password); //LogIn(Phone, Password);

        #endregion

        #region OpenWelcomeViewCommand

        private ICommand _OpenWelcomeViewCommand;

        public ICommand OpenWelcomeViewCommand => _OpenWelcomeViewCommand ??=
            new LambdaCommand(_ => _NavigationManager.Navigate(NavigationKeys.WelcomeView));

        #endregion

        #endregion

        #region Functions

        private bool CanLogIn() =>
            Phone.Length == 11 &&
            !string.IsNullOrWhiteSpace(Phone) &&
            !string.IsNullOrWhiteSpace(Password);

        private int CompleteCode;
        private void OnAuthorization(object sender, EventArgs<bool> e)
        {
            string text = CompleteCode switch
            {
                1 => "Неверный пароль",
                2 => "Вход успешно выполнен",
                0 => "Пользователь не найден",
                -1 => "Отсуствует соединение с базой данных",
                _ => ""
            };
            MessageBox.Show(text);
            if (CompleteCode == -1)
            {
                NavigationManager.Navigate(typeof(MainMenuView), new User
                {
                    Phone = Phone,
                    Password = Password,
                    Level = 1
                });
            }
        }

        #region old
        //private void LogIn(string phone, string password)
        //{
        //    if (Authorization.IsDatabaseConnected)
        //    {
        //        if (Authorization.IsUserExist(phone))
        //        {
        //            if (Authorization.ValidateUser(phone, password))
        //            {
        //                MessageBox.Show("Вход выполнен");
        //            }
        //            MessageBox.Show("Неверный пароль");
        //            return;
        //        }
        //        MessageBox.Show("Пользователь не существует");
        //        return;
        //    }
        //    MessageBox.Show("Отсутствует соединение к базе данных");
        //} 
        #endregion

        #endregion
    }
}
