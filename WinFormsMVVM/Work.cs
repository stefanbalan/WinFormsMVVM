using System.Threading;

namespace WinFormsMVVM
{
    public static class Work
    {
        public static void LongRunning()
        {
            Thread.Sleep(1000);
        }
    }
}
