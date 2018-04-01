using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace WinFormsMVVM
{
    public partial class Form1 : ViewBase<Form1ViewModel>
    {
        private WindowsFormsSynchronizationContext _uiContext = new WindowsFormsSynchronizationContext();
        public Form1()
        {
            InitializeComponent();
            UIContext.Init();
            var x = "";
            ViewModel = new Form1ViewModel();
            //Bind(v => v.FinishedGettingItems, () => chkFinished.Checked );
            Bind(vm => vm.Finished, v => ((Form1)v).chkFinished.Checked);
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

}
