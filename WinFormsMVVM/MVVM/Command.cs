using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;

namespace WinFormsMVVM
{
    public class Command : ICommand, INotifyPropertyChanged
    {
        private readonly Action _action;

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        private bool _enabled;
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Enabled"));
            }
        }

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
            var ctx = SynchronizationContext.Current;
            Enabled = false;
            var t = new Thread(() =>
            {
                _action();
                ctx.Post(state => { Enabled = true; }, null);
            });
            t.Start();
        }

        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}