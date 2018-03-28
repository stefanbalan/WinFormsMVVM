using System;
using System.Threading;

namespace WinFormsMVVM
{
    public static class UIContext
    {
        public static SynchronizationContext SyncCtx;
        public static void Init()
        {
            SyncCtx = SynchronizationContext.Current;
        }

        public static void Invoke(Action fc)
        {
            SyncCtx.Send(state => { fc(); }, null);
        }
    }
}
