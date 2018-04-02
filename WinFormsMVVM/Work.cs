using System.Threading;

namespace WinFormsMVVM
{
    public static class Work
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
    }
}
