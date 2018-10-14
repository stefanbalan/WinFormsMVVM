using System;
using System.ComponentModel;
using System.Threading;

namespace WinFormsMVVM.MVVM
{
    public class Command : INotifyPropertyChanged
    {
        private readonly Action _action;
        private bool _enabled;
        private string _name;

        public Command(Action action)
        {
            _action = action;
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Enabled"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Execute(object sender, object parameter)
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
    }
}