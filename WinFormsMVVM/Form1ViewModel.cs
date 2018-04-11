using System.Collections.ObjectModel;

namespace WinFormsMVVM
{
    public class Form1ViewModel : ViewModelBase
    {
        public ObservableCollection<string> Items { get; set; } = new ObservableCollection<string>();

        private bool _finishedGettingItems;
        public bool FinishedGettingItems
        {
            get { return _finishedGettingItems; }
            set { _finishedGettingItems = value; RaisePropertyChanged(); }
        }

        public Command Toggle;
        public Command GetItems;

        public Form1ViewModel()
        {
            Toggle = new Command(ToggleCommand) { Enabled = true, Name = "Toggle" };
            GetItems = new Command(GetItemsCommand) { Enabled = true, Name = "Get items" };

            Items.CollectionChanged += (sender, args) => RaisePropertyChanged("Items");
        }

        private void ToggleCommand()
        {
            FinishedGettingItems = !FinishedGettingItems;
        }

        private void GetItemsCommand()
        {
            Items.Clear();
            for (var i = 0; i < 10; i++)
            {
                Items.Add($"Item {i}");
                Work.LongRunning(); //simulate real life delay (database, service, etc...)
            }

            FinishedGettingItems = true;
        }

    }
}
