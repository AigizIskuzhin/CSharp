using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using MyBus.Infrastructure;
using MyBus.Infrastructure.Commands;
using MyBus.Models.User;

namespace MyBus.ViewModels
{
    public class EditClientViewModel : Base.ViewModel
    {
        public event EventHandler<EventArgs<bool>> Complete;
        private readonly Dictionary<string, object> _Values = new();
        private readonly Client Client;

        protected virtual bool SetValue(object value, [CallerMemberName] string propertyName = null)
        {
            if (_Values.TryGetValue(propertyName, out var old_value) && Equals(value, old_value))
                return false;
            _Values[propertyName] = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual T GetValue<T>(T defaultValue, [CallerMemberName] string propertyName = null)
        {
            if (_Values.TryGetValue(propertyName, out var value))
                return (T)value;
            return defaultValue;
        }

        public EditClientViewModel()
        {

        }
        public EditClientViewModel(Client client)
        {
            Client = client;
        }
        public int ID => Client.ID;
        public string Name { get => GetValue(Client.Name); set => SetValue(value); }
        public string Surname{ get => GetValue(Client.Surname); set => SetValue(value); }
        public string Patronymic{ get => GetValue(Client.Patronymic); set => SetValue(value); }
        public string Phone{ get => GetValue(Client.Phone); set => SetValue(value); }
        public DateTime Birthday{ get => GetValue(Client.Birthday); set => SetValue(value); }
        public int Level{ get => GetValue(Client.Level); set => SetValue(value); }
        public string Rank{ get => GetValue(Client.Rank); set => SetValue(value); }

        #region RestoreCommand

        private ICommand _RestoreCommand;
        public ICommand RestoreCommand => _RestoreCommand ??= new LambdaCommand(OnRestoreCommand, CanRestoreCommand);
        private bool CanRestoreCommand(object p) => _Values.Count!=0;

        private void OnRestoreCommand(object p)
        {
            Restore();
        }

        #endregion

        #region SaveCommand

        private ICommand _SaveCommand;
        public ICommand SaveCommand => _SaveCommand ??= new LambdaCommand(OnSaveCommand, CanSaveCommand);
        private bool CanSaveCommand(object p) => _Values.Count>0;

        private void OnSaveCommand(object p)
        {
            Save();
            Complete?.Invoke(this, true);
        }

        #endregion

        #region CloseCommand

        private ICommand _CloseCommand;
        public ICommand CloseCommand => _CloseCommand ??= new LambdaCommand(OnCloseCommand);

        private void OnCloseCommand(object p)
        {
            if (_Values.Count > 0)
            {
                MessageBoxResult msg = MessageBox.Show("Есть измененные поля, сохранить?", "Внимание",
                    MessageBoxButton.YesNoCancel);
                switch (msg)
                {
                    case MessageBoxResult.Yes:
                        Save();
                        Complete?.Invoke(this, true);
                        return;
                    case MessageBoxResult.Cancel:
                        return;
                }
            }
            Complete?.Invoke(this, false);
        }

        #endregion


        #region Functions

        private void Restore()
        {
            var properties = _Values.Keys.ToArray();
            _Values.Clear();
            foreach (var propertyName in properties) OnPropertyChanged(propertyName);
        }
        private void Save()
        {
            var type = Client.GetType();
            foreach (var (propertyName, value) in _Values)
            {
                var property = type.GetProperty(propertyName);
                if(property is null || !property.CanWrite)continue;
                
                property.SetValue(Client, value);
            }
            _Values.Clear();
        }
        #endregion



    }
}
