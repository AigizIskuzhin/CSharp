using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using MyBus.Infrastructure.Services.Validation;
using MyBus.Infrastructure.Services.Validation.Base;
using MySqlX.XDevAPI;

namespace MyBus.ViewModels.Base
{
    public class ValidatableViewModel : Base.ViewModel, INotifyDataErrorInfo
    {
        private ValidationService _ValidationService = new();
        private readonly Dictionary<string, IEnumerable> _Errors = new();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public IEnumerable GetErrors(string propertyName)
        {
            _Errors.TryGetValue(propertyName, out var errors);
            return errors;
        }

        public bool HasErrors => _Errors.Any();

        public void AddErrors(IEnumerable errors, [CallerMemberName] string propertyName = null)
        {
            _Errors[propertyName!] = errors;
            OnErrorsChanged(propertyName);
        }
        protected override bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            return base.Set(ref field, value, propertyName);
        }

        protected bool ValidateSet<T>(ref T field, T value, IEnumerable errors = null,[CallerMemberName] string propertyName = null)
        {
            AddErrors(errors, propertyName);
            return base.Set(ref field, value, propertyName);
        }

        public bool DeleteError(IEnumerable errors, [CallerMemberName] string propertyName = null) =>
            _Errors.Remove(propertyName!);

        public void OnErrorsChanged(string propertyName) =>
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

        public bool IsValid()
        {
            ErrorsChanged?.Invoke(this, null);
            return HasErrors;
        }
    }
}
