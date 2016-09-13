using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EnergieatlasLeibnitz.Classes.BaseClasses
{
    class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action _executeMethod;
        private Predicate<object> _canExecuteMethod;

        public RelayCommand(Action executeMethod, Predicate<object> canExecuteMethod)
        {
            this._executeMethod = executeMethod;
            this._canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteMethod == null)
            {
                return true;
            }

            bool retval = _canExecuteMethod(parameter);
            return retval;
        }

        public void Execute(object parameter)
        {
            _executeMethod();
        }
    }
}
