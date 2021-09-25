using System;
using System.Windows;
using MyBus.Infrastructure.Navigation.Base;
using MyBus.Models.User;
using MyBus.Models.User.Base;
using MyBus.ViewModels;
using MyBus.Views;
using MyBus.Views.Windows;

namespace MyBus.Infrastructure.Services
{
    public class AppWindowUserDialogService : Interfaces.IUserDialog
    {
        public AppWindowUserDialogService()
        {
            EditWindow = new EditWindow
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
        }

        private readonly EditWindow EditWindow;
        public bool Edit(User user)
        {
            switch (user)
            {
                case Client:
                    NavigationManager.Navigate(typeof(EditClientView), EditWindow.Frame, user);
                    var editClientViewModel = (EditClientViewModel)((FrameworkElement)EditWindow.Frame.Content).DataContext;
                    editClientViewModel.Complete += OnComplete;
                    break;
                case Worker:
                    NavigationManager.Navigate(typeof(EditWorkerView), EditWindow.Frame, user);
                    var editWorkerViewModel = (EditWorkerViewModel)((FrameworkElement)EditWindow.Frame.Content).DataContext;
                    editWorkerViewModel.Complete += OnComplete;
                    break;
                case Administrator:

                    break;
                default: return false;
            }
            return EditWindow.ShowDialog() ?? false;
        }

        private void OnComplete(object sender, EventArgs<bool> e)
        {
            EditWindow.DialogResult = e.Arg;
            EditWindow.Close();
        }

        public bool Add(Type rank)
        {
            
            switch (rank)
            {
            }
            return false;
        }
    }
}
