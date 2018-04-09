using System;
using System.Threading;
using System.Windows.Input;

namespace WinFormsMVVM
{
    public class Command : ICommand
    {

        public string Name { get; set; }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; CanExecuteChanged?.Invoke(this, EventArgs.Empty); }
        }

        private readonly Action _action;
        private bool _enabled;

        public Command(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return Enabled;
        }

        public void Execute(object parameter)
        {
            Enabled = false;
            var t = new Thread(() =>
            {
                _action();
                Enabled = true;
            });
            t.Start();
        }

        public event EventHandler CanExecuteChanged;
    }
}