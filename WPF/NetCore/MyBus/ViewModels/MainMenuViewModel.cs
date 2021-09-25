using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using core5;
using MyBus.Constants;
using MyBus.Infrastructure.Commands;
using MyBus.Infrastructure.Navigation;
using MyBus.Infrastructure.Navigation.Base;
using MyBus.Models;
using MyBus.Models.User;
using MyBus.Models.User.Base;
using MyBus.ViewModels.Base;
using MyBus.Views;

namespace MyBus.ViewModels
{
    class MainMenuViewModel : ViewModel, INavigationAware
    {
        public MainMenuViewModel()
        {
            App.LanguageChanged += AppOnLanguageChanged;
        }
        private NavigationManager _NavigationManager;
        public void OnNavigatingTo(NavigationManager navigationManager, params object[] args)
        {
            _NavigationManager = navigationManager;
            User = (User)args[0];
            NavigationList = GetNavigationList(User.Level);
        }

        private void AppOnLanguageChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(GetNavigationButtons));
        }

        #region Fields

        #region User
        private User _User;
        public User User
        {
            get => _User;
            set => Set(ref _User, value);
        }
        #endregion

        #region NavigationList
        private Dictionary<string, Type> _NavigationList;
        public Dictionary<string, Type> NavigationList
        {
            get => _NavigationList;
            set => Set(ref _NavigationList, value);
        }
        #endregion

        //#region NavigationList2
        //private Dictionary<string,NavigationButton> _NavigationList2;
        //public Dictionary<string,NavigationButton> NavigationList2
        //{
        //    get=> _NavigationList2 ?? GetNavigationList2(User.Level);
        //    set=> Set(ref _NavigationList2, value);
        //}
        //#endregion

        public IEnumerable<NavigationButton> GetNavigationButtons
        {
            get
            {
                switch (User.Level)
                {
                    case 1:
                        yield return new NavigationButton(NavigationKeys.RegistrationFormsView,
                            Application.Current.FindResource("BtnOpenRegFormsLoc")?.ToString(),
                            "Окно выбора формы регистрации пользователя");
                        yield return new NavigationButton(typeof(ClientAccountView),
                            Application.Current.FindResource("BtnOpenAccountLoc")?.ToString(),"Окно просмотра информации об аккаунте");
                        yield return new NavigationButton(NavigationKeys.WelcomeView,
                            Application.Current.FindResource("BtnExitLoc")?.ToString(), "Выход из программы в начальное окно");
                        break;
                }
            }
        }

        #endregion

        #region SelectedLanguage
        private CultureInfo _SelectedLanguage = App.Language;
        public CultureInfo SelectedLanguage
        {
            get => _SelectedLanguage;
            set
            {
                if (!Set(ref _SelectedLanguage, value)) return;
                App.Language = value;
            }
        }

        #endregion

        #region OpenViewCommand

        private ICommand _OpenViewCommand;
        public ICommand OpenViewCommand => _OpenViewCommand ??= new LambdaCommand(OnOpenViewCommand, CanOpenViewCommand);
        private bool CanOpenViewCommand(object p) => true;

        private void OnOpenViewCommand(object p)
        {
            _NavigationManager.Navigate((Type)p);
            if ((Type) p == NavigationKeys.WelcomeView) _NavigationManager.ClearCashedViews();
        }

        #endregion
        
        private Dictionary<string, Type> GetNavigationList(int userLevel)
        {
            return userLevel switch
            {
                1 => new() // Client
                {
                    ["Авторизация"] = NavigationKeys.LoginFormView, 
                    ["Формы регистрации"] = NavigationKeys.RegistrationFormsView,
                    ["Аккаунт"] = typeof(ClientAccountView),
                    ["Выйти"] = NavigationKeys.WelcomeView
                },
                2 => new() // Company worker
                {
                    ["Выйти"] = NavigationKeys.WelcomeView
                },
                3 => new() // Administrator
                {
                    ["Выйти"] = NavigationKeys.WelcomeView
                },
                _ => null
            };
        }

        //private Dictionary<string, NavigationButton> GetNavigationList2(int userLevel)
        //{
        //    return userLevel switch
        //    {
        //        1 => new()
        //        {
        //            ["Авторизация"] =
        //                new (NavigationKeys.LoginFormView, "Окно авторизации пользователя"),
        //            ["Формы регистрации"] = new (NavigationKeys.RegistrationFormsView, "Окно выбора формы регистрации пользователя"),
        //            ["Аккаунт"] = new(typeof(ClientAccountView),"Окно просмотра информации об аккаунте"),
        //            ["Выйти"] = new NavigationButton(NavigationKeys.WelcomeView, "Выход из программы в начальное окно")
        //        },
        //        _ => null
        //    };
        //}

    }
}
