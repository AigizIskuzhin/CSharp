using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using MyBus.Infrastructure.Commands;
using MyBus.Infrastructure.Navigation;
using MyBus.Infrastructure.Navigation.Base;
using MyBus.Infrastructure.Services;
using MyBus.Infrastructure.Services.Interfaces;
using MyBus.Infrastructure.Tests;
using MyBus.Models.User;
using MyBus.Models.User.Base;
using MyBus.ViewModels.Base;
using MyBus.Views;
using MyBus.Views.Windows;
using MySql.Data.MySqlClient;

namespace MyBus.ViewModels
{
    public class MainViewModel :  ViewModel, INavigationAware
    {
        private NavigationManager _NavigationManager;
        public void OnNavigatingTo(NavigationManager navigationManager, params object[] args)
        {
            _NavigationManager = navigationManager;
        }

        private MySqlConnection Connection;
        public MainViewModel()
        {
            Users = TestData.Data.Users;
            _UsersView.Filter += OnUsersViewFiltred;
            //_UsersView.GroupDescriptions.Add(new PropertyGroupDescription("Level"));
            //_UsersView.SortDescriptions.Add(new SortDescription("Level", ListSortDirection.Descending));
        }

        private IUserDialog IUserDialog;

        #region EditSelectedUserCommand

        private ICommand _EditSelectedUserCommand;
        public ICommand EditSelectedUserCommand => _EditSelectedUserCommand ??= new LambdaCommand(OnEditSelectedUserCommand, CanEditSelectedUserCommand);
        private bool CanEditSelectedUserCommand(object p) => SelectedUser!=null;

        private void OnEditSelectedUserCommand(object p)
        {
            var user = (User)p;

            IUserDialog ??= new AppWindowUserDialogService();

            if (IUserDialog.Edit(user))
            {
                _UsersView.View.Refresh();
            }
            else
            {

            }
        }

        #endregion
        #region SelectedUser
        private User _SelectedUser;
        public User SelectedUser
        {
            get=> _SelectedUser;
            set=> Set(ref _SelectedUser, value);
        }
        #endregion
        #region Users
        private List<User> _Users;
        public List<User> Users
        {
            get=> _Users;
            set
            {
                if (!Set(ref _Users, value)) return;
                _UsersView.Source = value;
                //OnPropertyChanged(nameof(UsersView));
            }
        }

        #endregion
        #region UsersFilterText
        private string _UsersFilterText;
        public string UsersFilterText
        {
            get=> _UsersFilterText;
            set
            {
                if (!Set(ref _UsersFilterText, value)) return;
                //OnPropertyChanged(nameof(UsersView));
                //OnPropertyChanged(nameof(_UsersView));
                //OnPropertyChanged(nameof(UsersView));
                _UsersView.View.Refresh();
                //test2();
            }
        }

        private async void test2()
        {
            await Task.Run(() =>
                _UsersView.View.Refresh());
        }
        #endregion
        #region UsersView

        private User _User;
        private Client _Client;
        private Worker _Worker;
        private Administrator _Administrator;
        private void OnUsersViewFiltred(object sender, FilterEventArgs e)
        {
            _User = (User)e.Item;

            if (string.IsNullOrWhiteSpace(UsersFilterText))
                return;
                
            string filter = UsersFilterText;

            switch (_User)
            {
                case Client client: _Client = client;
                    break;
                case Worker worker: _Worker = worker;
                    break;
                case Administrator administrator: _Administrator = administrator;
                    break;
            }


            if (filter.Split().Length > 1)
            {
                foreach (string keyword in filter.Split())
                {
                    if(string.IsNullOrWhiteSpace(keyword)) continue;
                    if (_User.Phone.Contains(keyword)) continue;
                    if (_User.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                        continue;
                    if (_User.Rank.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                        continue;


                    switch (_User)
                    {
                        case Client:
                            if(_Client.Surname.Contains(keyword, StringComparison.OrdinalIgnoreCase))continue;
                            if(_Client.Patronymic.Contains(keyword, StringComparison.OrdinalIgnoreCase)) continue;
                            var t = _Client.Birthday.ToShortDateString();
                            if (_Client.Birthday.ToShortDateString().Contains(keyword, StringComparison.OrdinalIgnoreCase))continue;
                            break;
                        case Worker:
                            break;
                        case Administrator:
                            break;
                    }




                    e.Accepted = false;
                    return;
                }
                return;
            }

            if (_User.Phone.Contains(filter)) return;
            if (_User.Name.Contains(filter, StringComparison.OrdinalIgnoreCase))
                return;
            if (_User.Rank.Contains(filter, StringComparison.OrdinalIgnoreCase))
                return;


            e.Accepted = false;
        }
        private readonly CollectionViewSource _UsersView = new();
        public ICollectionView UsersView => _UsersView.View;
        #endregion

        #region TestList
        private List<string> _TestList = new();
        public List<string> TestList
        {
            get=> _TestList;
            set=> Set(ref _TestList, value);
        }
        #endregion

        #region EditUserCommand

        private ICommand _EditUserCommand;
        public ICommand EditUserCommand => _EditUserCommand ??= new LambdaCommand(OnEditUserCommand, CanEditUserCommand);
        private bool CanEditUserCommand(object p) => true;

        private void OnEditUserCommand(object p)
        {
            var user = (User) p;
            if (IUserDialog.Edit(user))
            {

            }
            else
            {

            }
        }

        #endregion

        #region DeleteUserCommand

        private ICommand _DeleteUserCommand;
        public ICommand DeleteUserCommand => _DeleteUserCommand ??= new LambdaCommand(OnDeleteUserCommand, CanDeleteUserCommand);
        private bool CanDeleteUserCommand(object p) => true;

        private void OnDeleteUserCommand(object p)
        {

            Users.Remove((User)p);
            //var callingDispatcher = Dispatcher.CurrentDispatcher;
            //ThreadPool.QueueUserWorkItem(
            //    (state) =>
            //    {
            //        Users.Remove((User)p);
            //        //   Вызов действия.

            //        ////  Fire the executed event and set the executing state.
            //        //ReportProgress(
            //        //    () =>
            //        //    {
            //        //        //  Больше не в процессе выполнения.
            //        //        IsExecuting = false;

            //        //        //  если отменили,
            //        //        //  вызвать событие отмены - , если нет – продолжить выполнение.
            //        //        if (IsCancellationRequested)
            //        //            InvokeCancelled(new CommandEventArgs() { Parameter = param });
            //        //        else
            //        //            InvokeExecuted(new CommandEventArgs() { Parameter = param });

            //        //        //  Юольше не запрашиваем отмену.
            //        //        IsCancellationRequested = false;
            //        //    }
            //        //);
            //    }
            //);
            UsersView.Refresh();
            //callingDispatcher.Invoke(UsersView.Refresh);
        }

        #endregion

        #region TestCommand

        private ICommand _TestCommand;
        public ICommand TestCommand => _TestCommand ??= new LambdaCommand(OnTestCommand);

        private async void OnTestCommand(object p)
        {
            await Task.Run(Test);

        }

        #region test
        private List<string> _test = new ();
        public List<string> test
        {
            get=> _test;
            set
            {
                _test.Add(value[0]);
                TestList = _test;
            }
        }

        #endregion
        private void Test()
        {
            //var list = new List<string>();
            ////for (int i = 0; i < 10000; i++)
            ////{
            ////    new MySqlCommand($"insert into table1 (table1col) values({i})", Connection).ExecuteNonQuery();
            ////}
            //var reader = new MySqlCommand("Select * from table1", Connection).ExecuteReader();
            //while (reader.Read())
            //{
            //    test = new List<string> {reader[1].ToString()};
            //    Thread.Sleep(1000);
            //}
            //reader.Close();
            ////return list;
        }

        #endregion

    }
}
