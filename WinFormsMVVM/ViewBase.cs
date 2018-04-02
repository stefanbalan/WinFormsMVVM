using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace WinFormsMVVM
{
    //todo maybe get rid of ViewBase and use BindManager instead?
    public class ViewBase<TViewModel> : Form where TViewModel : ViewModelBase, new()
    {
        internal TViewModel ViewModel = new TViewModel();
        internal Dictionary<string, PropertyChangedHandler<TViewModel>> PropertyBindings
            = new Dictionary<string, PropertyChangedHandler<TViewModel>>();


        internal Dictionary<string, ICommandBinder> CommandBindings = new Dictionary<string, ICommandBinder>();

        //protected void Bind<T>(Expression<Func<TViewModel, T>> expression, Expression<Func<ViewBase<TViewModel>, T>> viewProperty)
        //{
        //    if (!(expression.Body is MemberExpression member))
        //        throw new ArgumentException($"Expression '{expression.Name}' refers to a method, not a property.");

        //    var propInfo = member.Member as PropertyInfo;
        //    if (propInfo == null)
        //        throw new ArgumentException($"Expression '{expression.Name}' refers to a field, not a property.");

        //    if (propInfo.ReflectedType == null ||
        //        typeof(TViewModel) != propInfo.ReflectedType && !typeof(TViewModel).IsSubclassOf(propInfo.ReflectedType))
        //        throw new ArgumentException($"Expresion '{expression}' refers to a property that is not from type {typeof(TViewModel).Name}.");



        //    if (!(viewProperty.Body is MemberExpression dstmember))
        //        throw new ArgumentException($"Expression '{viewProperty.Name}' refers to a method, not a property.");

        //    var vpropInfo = dstmember.Member as PropertyInfo;
        //    if (vpropInfo == null)
        //        throw new ArgumentException($"Expression '{viewProperty.Name}' refers to a field, not a property.");

        //    var sm = vpropInfo.SetMethod;
        //    //sm.Invoke(this, (object[]) propInfo.GetValue(null));
        //    sm.Invoke(this, new object[] { true });

        //    PropertyBindings.Add(propInfo.Name, null);
        //}

        protected void Bind<T>(Expression<Func<TViewModel, T>> sourceExpression, Action<T> setterDelegate)
        {
            var member = sourceExpression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException($"Expression '{sourceExpression.Name}' refers to a method, not a property.");

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException($"Expression '{sourceExpression.Name}' refers to a field, not a property.");

            if (propInfo.ReflectedType == null ||
                typeof(TViewModel) != propInfo.ReflectedType && !typeof(TViewModel).IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException($"Expresion '{sourceExpression}' refers to a property that is not from type {typeof(TViewModel).Name}.");

            var sourceGetter = sourceExpression.Compile();

            PropertyBindings.Add(propInfo.Name, new PropertyChangedHandler<TViewModel>(SynchronizationContext.Current, (vm) =>
            {
                var val = sourceGetter.Invoke(vm);
                setterDelegate.Invoke(val);
            }));
        }



        public ViewBase()
        {
            ViewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(eventArgs.PropertyName)) return;
            if (!PropertyBindings.ContainsKey(eventArgs.PropertyName)) return;

            var handler = PropertyBindings[eventArgs.PropertyName];
            handler.Execute(sender as TViewModel);
        }
    }
}