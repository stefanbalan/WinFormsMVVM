using System.Collections.ObjectModel;
using System.Windows.Input;

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

        public ICommand Toggle;
        public ICommand GetItems;


        public Form1ViewModel()
        {
            Items = new ObservableCollection<string>() { "item1", "item2" };
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
            for (var i = 0; i < 15; i++)
            {
                //UIContext.Invoke();
                Items.Add($"Item {i}");
                Work.LongRunning();
            }

            FinishedGettingItems = true;
        }

    }
}
