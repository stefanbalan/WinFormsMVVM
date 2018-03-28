using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using WinFormsMVVM.Annotations;

namespace WinFormsMVVM
{
    public partial class Form1 : Form
    {
        private WindowsFormsSynchronizationContext _uiContext = new WindowsFormsSynchronizationContext();
        public Form1()
        {
            InitializeComponent();
            UIContext.Init();
        }


        private void BtnBlocking_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < 15; i++)
            {
                cmbTest.Items.Add($"Item {i}");
                Work.LongRunning();
            }
        }

        private void BtnNonBlocking_Click(object sender, EventArgs e)
        {
           
        }
    }


    public class WorkerViewModel  : INotifyPropertyChanged
    {
        public WorkerViewModel()
        {
            
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
