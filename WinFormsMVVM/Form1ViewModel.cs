using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
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

        public IList<string> Items { get; set; }
        public bool FinishedGettingItems { get; set; }
        public bool Finished;

        public static void JobGetItems()
        {
            for (var i = 0; i < 15; i++)
            {
                //UIContext.Invoke();
                //cmbTest.Items.Add($"Item {i}");
                Work.LongRunning();
            }


        }

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

    public class ViewBase<TViewModel> : Form where TViewModel : ViewModelBase, new()
    {
        internal ViewModelBase ViewModel;
        internal Dictionary<string, PropertyChangedHandler> PropertyBindings = new Dictionary<string, PropertyChangedHandler>();

        protected void Bind<T>(Expression<Func<TViewModel, T>> expression, Expression<Func<ViewBase<TViewModel >,T>> viewProperty)
        {
            if (!(expression.Body is MemberExpression member))
                throw new ArgumentException($"Expression '{expression.Name}' refers to a method, not a property.");

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException($"Expression '{expression.Name}' refers to a field, not a property.");

            if (propInfo.ReflectedType == null ||
                typeof(TViewModel) != propInfo.ReflectedType && !typeof(TViewModel).IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException($"Expresion '{expression}' refers to a property that is not from type {typeof(TViewModel).Name}.");



            if (!(viewProperty.Body is MemberExpression dstmember))
                throw new ArgumentException($"Expression '{viewProperty.Name}' refers to a method, not a property.");

            var vpropInfo = dstmember.Member as PropertyInfo;
            if (vpropInfo == null)
                throw new ArgumentException($"Expression '{viewProperty.Name}' refers to a field, not a property.");

            var sm = vpropInfo.SetMethod;
            //sm.Invoke(this, (object[]) propInfo.GetValue(null));
            sm.Invoke(this, new object[] { true });

            PropertyBindings.Add(propInfo.Name, null);

        }

        public ViewBase()
        {
            ViewModel = new TViewModel();
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
            SyncContext.Post(state => HandlerAction(), null);
        }

    }
}
