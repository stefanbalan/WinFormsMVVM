using System;
using System.ComponentModel;
using System.Threading;

namespace WinFormsMVVM
{
    public class Command : INotifyPropertyChanged
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Enabled"));
            }
        }

        public Command(Action action)
        {
            _action = action;
        }

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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}