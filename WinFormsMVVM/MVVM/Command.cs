using System;
using System.Threading;
using System.Windows.Input;

namespace WinFormsMVVM
{
    public class Command : ICommand
    {

        public string Name { get; set; }
        public bool Enabled { get; set; }

        private readonly Action _action;

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
            var t = new Thread(() => _action());
            t.Start();
        }

        public event EventHandler CanExecuteChanged;
    }
}