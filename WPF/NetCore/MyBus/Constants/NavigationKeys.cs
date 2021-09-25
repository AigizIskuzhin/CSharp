using System;
using System.Collections.Generic;
using MyBus.ViewModels;
using MyBus.Views;

namespace MyBus.Constants
{
    public static class NavigationKeys
    {
        public static readonly Type WelcomeView = typeof(Views.WelcomeView);
        public static readonly Type LoginFormView = typeof(Views.LoginFormView);
        public static readonly Type MainMenuView = typeof(Views.MainMenuView);
        public static readonly Type RegistrationFormsView = typeof(Views.RegistrationFormsView);
        public static readonly Type MainView = typeof(Views.MainView);
        public static readonly Type ReportConstructorView = typeof(Views.ReportConstructorView);

        public static readonly Dictionary<Type, Type> NavigationDictionary = new()
        {
            // Начальное окно
            [WelcomeView] = typeof(WelcomeViewModel),
            // Окно выбора формы регистрации
            [RegistrationFormsView] = typeof(RegistrationFormsViewModel),
            // Окно авторизации
            [LoginFormView] = typeof(LoginFormViewModel),
            // Главное окно
            [MainView] = typeof(MainViewModel),
            // Главное окно навигации (меню)
            [MainMenuView] = typeof(MainMenuViewModel),
            // Окно конструктора отчетов
            [ReportConstructorView] = typeof(ReportConstructorViewModel),
            [typeof(Views.ClientAccountView)] = typeof(ClientAccountViewModel),
            // Окно редактирования клиента
            [typeof(EditClientView)] = typeof(EditClientViewModel),
            // Окно редактирования сотрудника
            [typeof(EditWorkerView)] = typeof(EditWorkerViewModel),
        };
    }
}
