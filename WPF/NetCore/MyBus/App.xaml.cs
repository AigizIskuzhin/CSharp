using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using MyBus.Infrastructure.Navigation.Base;
using MyBus.Infrastructure.Services.MySql;
using MyBus.Models.User;
using MyBus.Views;
using MyBus.Views.Windows;

namespace MyBus
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>  
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            LanguageChanged += OnLanguageChanged;
            LaunchApp();
            base.OnStartup(e);
            Language = MyBus.Properties.Settings.Default.DefaultLanguage;
        }

        public static event EventHandler LanguageChanged;

        public static MySqlService MySqlService = new ();
        public static CultureInfo Language
        {
            get => System.Threading.Thread.CurrentThread.CurrentUICulture;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (Equals(value, System.Threading.Thread.CurrentThread.CurrentUICulture)) return;


                MyBus.Properties.Settings.Default.DefaultLanguage = value;
                System.Threading.Thread.CurrentThread.CurrentUICulture = value;


                ResourceDictionary dict = new()
                {
                    Source = value.Name switch
                    {
                        "ru-RU" => new Uri($"Resources/Localization/lang.{value.Name}.xaml", UriKind.Relative),
                        _ => new Uri("Resources/Localization/lang.xaml", UriKind.Relative)
                    }
                };

                var oldDictionary = (from dictionary in Current.Resources.MergedDictionaries
                                     where dictionary.Source != null &&
                                           dictionary.Source.OriginalString.StartsWith("Resources/Localization/lang.")
                                     select dictionary).First();
                if (oldDictionary != null)
                {
                    int ind = Current.Resources.MergedDictionaries.IndexOf(oldDictionary);
                    Current.Resources.MergedDictionaries.Remove(oldDictionary);
                    Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else Current.Resources.MergedDictionaries.Add(dict);


                LanguageChanged?.Invoke(Current, new EventArgs());
            }
        }

        private static void OnLanguageChanged(object sender, EventArgs e) => MyBus.Properties.Settings.Default.Save();

        private static void LaunchApp()
        {
            MainWindow window2 = new();
            NavigationManager navManager2 = new(window2.Frame);
            var user = new Client { Level = 1 };
            navManager2.Navigate(typeof(MainMenuView), user);
            window2.Show();
        }
    }
}
