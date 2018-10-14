using System;
using System.Threading;

namespace WinFormsMVVM.MVVM
{
    public class PropertyChangedHandler<TViewModel>
        where TViewModel : ViewModelBase
    {
        protected readonly Action<TViewModel> HandlerAction;
        protected readonly SynchronizationContext SyncContext;

        public PropertyChangedHandler(SynchronizationContext context, Action<TViewModel> action)
        {
            SyncContext = context;
            HandlerAction = action;
        }

        public void Execute(TViewModel vm)
        {
            SyncContext.Post(state => HandlerAction(vm), null);
        }
    }
}