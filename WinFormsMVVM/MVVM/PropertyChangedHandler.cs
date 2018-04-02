using System;
using System.Threading;

namespace WinFormsMVVM
{
    public class PropertyChangedHandler<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly SynchronizationContext SyncContext;
        private readonly Action<TViewModel> HandlerAction;
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