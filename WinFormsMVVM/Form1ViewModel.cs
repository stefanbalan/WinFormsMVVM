using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;

namespace WinFormsMVVM
{
    public class Form1ViewModel : ViewModelBase
    {
        public IList<string> Items { get; set; }

        public bool FinishedGettingItems
        {
            get { return _finishedGettingItems; }
            set { _finishedGettingItems = value; RaisePropertyChanged(); }
        }

        public bool Finished;
        public ICommand Toggle;
        private bool _finishedGettingItems;

        public Form1ViewModel()
        {
            Toggle = new Command(ToggleCommand) { Enabled = true, Name = "Toggle" };
        }

        public void ToggleCommand()
        {
            FinishedGettingItems = !FinishedGettingItems;
        }

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
