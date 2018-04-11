using System;
using System.Threading;

namespace WinFormsMVVM
{
    public class PropertyChangedHandler<TViewModel>
        where TViewModel : ViewModelBase
    {
        protected readonly SynchronizationContext SyncContext;
        protected readonly Action<TViewModel> HandlerAction;

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