using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JewelryApp
{
    public class MainWindowViewModel : INotifyDataErrorInfo
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || (!HasErrors))
                return null;
            return new List<string>() { "Invalid credentials." };
        }

        public bool HasErrors { get; set; } = false;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool CheckCredentials()
        {
            //HasErrors = !new LogInService().LogIn(Email, Pass);
            if (HasErrors)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("Email"));
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("Pass"));
            }
            else
            {
                return true;
            }
            return false;
        }
    }
}
