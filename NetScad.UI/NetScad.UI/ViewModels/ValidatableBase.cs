using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NetScad.UI.ViewModels
{
	public abstract class ValidatableBase : ReactiveObject, INotifyDataErrorInfo
	{
        protected readonly Dictionary<string, List<string>> _errors = new();

        public bool HasErrors => _errors.Any();

        public IEnumerable GetErrors(string? propertyName)
        {
            return propertyName != null && _errors.ContainsKey(propertyName)
                ? _errors[propertyName]
                : Enumerable.Empty<string>();
        }

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        protected void ValidateProperty<T>(T value, [CallerMemberName] string? propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName)) return;

            _errors.Remove(propertyName);
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(this) { MemberName = propertyName };
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            if (!System.ComponentModel.DataAnnotations.Validator.TryValidateProperty(value, validationContext, results))
            {
                _errors[propertyName] = results.Select(r => r.ErrorMessage ?? "").ToList();
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        protected void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors[propertyName] = new List<string>();
            }
            _errors[propertyName].Add(error);
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void ClearErrors([CallerMemberName] string? propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                _errors.Clear();
            }
            else if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
            }
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}