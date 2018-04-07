using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WinFormsMVVM
{
    public class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging, INotifyCollectionChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void RaisePropertyChanging([CallerMemberName] string propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;


    }

    //public class CommandBinder : ICommandExecutor

}