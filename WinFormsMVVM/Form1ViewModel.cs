using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace WinFormsMVVM
{
    public class Form1ViewModel : ViewModelBase
    {
        public static void LongRunning()
        {
            Thread.Sleep(10000);
        }

        public static void JobGetItems()
        {
            for (var i = 0; i < 15; i++)
            {
                //UIContext.Invoke();
                //cmbTest.Items.Add($"Item {i}");
                Work.LongRunning();
            }


        }

        public IList<string> Items { get; set; }
    }

    public class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging
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
    }

    public class ViewBase : Form
    {
        internal ViewModelBase ViewModel;
        internal Dictionary<string, PropertyChangedHandler> PropertyBindings;

        public ViewBase()
        {
            ViewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(eventArgs.PropertyName)) return;
            if (!PropertyBindings.ContainsKey(eventArgs.PropertyName)) return;

            var handler = PropertyBindings[eventArgs.PropertyName];
            handler.Execute();
        }
    }

    internal class PropertyChangedHandler
    {
        private readonly SynchronizationContext SyncContext;
        private readonly Action HandlerAction;
        public PropertyChangedHandler(SynchronizationContext context, Action action)
        {
            SyncContext = context;
            HandlerAction = action;
        }

        public void Execute()
        {
            SyncContext.Post( state => HandlerAction(), null);
        }

    }
}
